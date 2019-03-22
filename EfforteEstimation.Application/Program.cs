using HandleTrelloBoard.Entities;
using HandleTrelloBoard.Services;
using ReadTextFile.Entities;
using ReadTextFile.Entities.Config;
using ReadTextFile.Log;
using ReadTextFile.Services;
using System.Linq;

namespace EfforteEstimation.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            const string separator = "=================================================================================================================================================";

            SharedLog sharedLog = new SharedLog();

            sharedLog.WriteLog(separator, "", SharedLog.FileName.Date, SharedLog.LogType.Message);
            sharedLog.WriteLog("Iniciando processo...", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            sharedLog.WriteLog("Recuperando informações de configuração.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            // Recupera configurações necessárias.
            ConfigProperties configProperties =
                new ConfigProperties().readJsonConfigFile();

            if (!configProperties.Valid)
            {
                sharedLog.WriteLog(configProperties.Notifications.FirstOrDefault().Message, string.Empty, SharedLog.FileName.Date, SharedLog.LogType.Error);
                endProcess(sharedLog);
                return;
            }

            // Realiza todas validações necessárias para garantir que a classe está consistente.
            configProperties.ValidateReadingTextFile();

            if (configProperties.ReadingTextFile.Invalid)
            {
                sharedLog.WriteLog(configProperties.Notifications.FirstOrDefault().Message, "", SharedLog.FileName.Date, SharedLog.LogType.Error);
                endProcess(sharedLog);
                return;
            }

            sharedLog.WriteLog($@"Informações recuperadas com sucesso: [Caminho do arquivo:] {configProperties.ReadingTextFile.FilePath} [Nome do arquivo:] {configProperties.ReadingTextFile.FileName}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            sharedLog.WriteLog("Iniciando leitura do arquivo para recuperação das informações.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            Estimate estimate = new Estimate();
            estimate.Topics = ReadFileText.readTextFileToObject(configProperties);

            // Recupera o primeiro tópico que possa estar com erro.
            var topicWithError = estimate.Topics.Where(t => t.Invalid).ToList().FirstOrDefault();

            if (topicWithError != null)
            {

                sharedLog.WriteLog(topicWithError.Notifications.FirstOrDefault().Message, "", SharedLog.FileName.Date, SharedLog.LogType.Error);
                endProcess(sharedLog);
                return;
            }

            if (estimate.Topics == null)
            {
                sharedLog.WriteLog("Erro ao realizar leitura do arquivo.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
                endProcess(sharedLog);
                return;
            }
            else
                sharedLog.WriteLog("Leitura do arquivo e recuperação das informações realizado com sucesso.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            // Calcula tempo total para desenvolvimento.
            estimate.sumHoursDevelopment();

            // Calcula tempo total para testes
            estimate.sumHoursTest();

            sharedLog.WriteLog($@"Horas desenvolvimento: {estimate.TotalHoursDevelopment}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
            sharedLog.WriteLog($@"Horas teste: {estimate.TotalHoursTest}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            sharedLog.WriteLog($@"Concluído processo de análise da estimativa de horas.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);




            #region Processo para o Quadro.

            sharedLog.WriteLog($@"Iniciando processo para manipulação do quadro.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);


            // Valida se está configurado para manipular o quadro.
            if (!configProperties.CreateBoard.Create)
            {
                sharedLog.WriteLog("Não está configurado para realizar a manipulação do quadro.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
                endProcess(sharedLog);
                return;
            }

            Authentication authentication = new Authentication(configProperties.CreateBoard.Key, configProperties.CreateBoard.Token);
            var trello = authentication.Authenticate();

            if (authentication.Invalid)
            {
                sharedLog.WriteLog($"Erro ao realizar autenticação do processo. Retorno: {authentication.Notifications.FirstOrDefault().Message}.", "", SharedLog.FileName.Date, SharedLog.LogType.Error);
                endProcess(sharedLog);
                return;
            }

            sharedLog.WriteLog($"Iniciando processo de atualização do quadro.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            var updateTemplateBoard = new UpdateTemplateBoard
                (
                    configProperties.CreateBoard.Key,
                    configProperties.CreateBoard.Token,
                    configProperties.CreateBoard.IdBoard, 
                    estimate, 
                    trello
                 );

            UpdateBoard updateBoard = new UpdateBoard();

            updateBoard.UpdateTemplateBoard(updateTemplateBoard);

            if(updateTemplateBoard.Invalid)
            {
                sharedLog.WriteLog($"Erro ao realizar atualização do quadro. Retorno: {updateTemplateBoard.Notifications.FirstOrDefault().Message}.", "", SharedLog.FileName.Date, SharedLog.LogType.Error);
                endProcess(sharedLog);
                return;
            }

            sharedLog.WriteLog($"Concluído processo de criação dos cartões no quadro {updateTemplateBoard.TemplateBoard.Name}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
            sharedLog.WriteLog($"Cartões criados:", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            int countCards = 0;

            foreach (var card in updateTemplateBoard.Cards)
            {
                countCards++;
                sharedLog.WriteLog($"{countCards} - {card.Name}", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
            }

            sharedLog.WriteLog($"Concluído processo de atualização do quadro {updateTemplateBoard.TemplateBoard.Name}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
                       

            #endregion


            endProcess(sharedLog);

        }

        public static void endProcess(SharedLog sharedLog)
        {
            sharedLog.WriteLog($@"Processo finalizado.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
        }
    }

}
