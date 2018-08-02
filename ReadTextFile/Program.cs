using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReadTextFile.Entities;
using ReadTextFile.Entities.Config;
using ReadTextFile.Constants;
using ReadTextFile.Log;
using ReadTextFile.Services;

namespace ReadTextFile
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

            if (!configProperties.validationResults.IsValid)
            {
                sharedLog.WriteLog(configProperties.validationResults.Errors.FirstOrDefault().Message, string.Empty, SharedLog.FileName.Date, SharedLog.LogType.Error);
                endProcess(sharedLog);
                return;
            }

            // Realiza todas validações necessárias para garantir que a classe está consistente.
            configProperties.validate();

            if (!configProperties.validationResults.IsValid)
            {
                sharedLog.WriteLog(configProperties.validationResults.Errors.FirstOrDefault().Message, "", SharedLog.FileName.Date, SharedLog.LogType.Error);
                endProcess(sharedLog);
                return;
            }

            sharedLog.WriteLog($@"Informações recuperadas com sucesso: [Caminho do arquivo:] {configProperties.FilePath} [Nome do arquivo:] {configProperties.FileName}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            sharedLog.WriteLog("Iniciando leitura do arquivo para recuperação das informações.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            Estimate estimate = new Estimate();
            estimate.Topics = ReadFileText.readTextFileToObject(configProperties);

            // Recupera o primeiro tópico que possa estar com erro.
            var topicWithError = estimate.Topics.Where(t => !t.validationResults.IsValid).ToList().FirstOrDefault();

            if (topicWithError != null)
            {
                // Caso erro seja por valor inválido no tempo para teste ou desenvolvimento, adiciona à mensagem de erro a descrição do tópico.
                if (topicWithError.validationResults.Errors.FirstOrDefault().Code == ErrosCode.HorasDesenvolvimentoInvalida ||
                        topicWithError.validationResults.Errors.FirstOrDefault().Code == ErrosCode.HorasTesteInvalida)
                    topicWithError.validationResults.Errors.FirstOrDefault().Message += $" Tópico: {topicWithError.Description}";

                sharedLog.WriteLog(topicWithError.validationResults.Errors.FirstOrDefault().Message, "", SharedLog.FileName.Date, SharedLog.LogType.Error);
                endProcess(sharedLog);
                return;

            }
            
            if (estimate.Topics == null)
                sharedLog.WriteLog("Erro ao realizar leitura do arquivo.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
            else
                sharedLog.WriteLog("Leitura do arquivo e recuperação das informações realizado com sucesso...", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            SumHoursDevelopment sumHoursDevelopment = new SumHoursDevelopment();

            decimal d = sumHoursDevelopment.SumHoursDevelopmentOfTopics(estimate.Topics);

            sharedLog.WriteLog($@"Horas desenvolvimento: {d}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            endProcess(sharedLog);

        }

        public static void endProcess(SharedLog sharedLog)
        {
            sharedLog.WriteLog($@"Processo finalizado.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
        }
    }
}
