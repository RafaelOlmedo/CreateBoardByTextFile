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
                sharedLog.WriteLog("Erro ao realizar leitura do arquivo.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
            else
                sharedLog.WriteLog("Leitura do arquivo e recuperação das informações realizado com sucesso.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            // Calcula tempo total para desenvolvimento.
            estimate.sumHoursDevelopment();

            // Calcula tempo total para testes
            estimate.sumHoursTest();

            sharedLog.WriteLog($@"Horas desenvolvimento: {estimate.TotalHoursDevelopment}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
            sharedLog.WriteLog($@"Horas teste: {estimate.TotalHoursTest}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            endProcess(sharedLog);

        }

        public static void endProcess(SharedLog sharedLog)
        {
            sharedLog.WriteLog($@"Processo finalizado.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
        }
    }

}
