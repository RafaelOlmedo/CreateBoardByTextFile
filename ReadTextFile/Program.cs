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

            List<Topic> lstTopics = ReadFileText.readTextFileToObject(configProperties);

            var v = lstTopics.Where(t => !t.validationResults.IsValid).ToList();

            if (v.Count() > 0)
            {
                if(v.FirstOrDefault().validationResults.Errors.FirstOrDefault().Code == ErrosCode.HorasDesenvolvimentoInvalida)
                {
                    sharedLog.WriteLog(v.FirstOrDefault().validationResults.Errors.FirstOrDefault().Message + $@"Tópico: {v.FirstOrDefault().Descricao}", "", SharedLog.FileName.Date, SharedLog.LogType.Error);
                    endProcess(sharedLog);
                    return;
                }
                
            }

            // TODO - Realizar tratamento.
            if (lstTopics == null)
                sharedLog.WriteLog("Erro ao realizar leitura do arquivo.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
            else
                sharedLog.WriteLog("Leitura do arquivo e recuperação das informações realizado com sucesso...", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            SumHoursDevelopment sumHoursDevelopment = new SumHoursDevelopment();

            decimal d = sumHoursDevelopment.SumHoursDevelopmentOfTopics(lstTopics);

            sharedLog.WriteLog($@"Horas desenvolvimento: {d}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

        }

        public static void endProcess(SharedLog sharedLog)
        {
            sharedLog.WriteLog($@"Finalizando processo.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

        }
    }
}
