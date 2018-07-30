using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReadTextFile.Entities;
using ReadTextFile.Entities.Config;
using ReadTextFile.Log;
using ReadTextFile.Services;

namespace ReadTextFile
{
    class Program
    {
        static void Main(string[] args)
        {
            const string separator = "=========================================================================================================================================================";
            
            SharedLog sharedLog = new SharedLog() ;

            sharedLog.WriteLog(separator, "", SharedLog.FileName.Date, SharedLog.LogType.Message);
            sharedLog.WriteLog("Iniciando processo...", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            sharedLog.WriteLog("Recuperando informações de configuração.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            // Recupera configurações necessárias.
            ConfigProperties configProperties =
                new ConfigProperties().readJsonConfigFile();

            if (string.IsNullOrEmpty(configProperties.FilePath) ||
                string.IsNullOrEmpty(configProperties.FileName))
            {
                // TODO - Incluir tratamento.

            }

            sharedLog.WriteLog($@"Informações recuperadas com sucesso: [Caminho do arquivo:] {configProperties.FilePath} [Nome do arquivo:] {configProperties.FileName}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            sharedLog.WriteLog("Iniciando leitura do arquivo para recuperação das informações.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            List<Topic> lstTopics = ReadFileText.readTextFileToObject(configProperties);

            // TODO - Realizar tratamento.
            if(lstTopics == null)
                sharedLog.WriteLog("Erro ao realizar leitura do arquivo.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);
            else
                sharedLog.WriteLog("Leitura do arquivo e recuperação das informações realizado com sucesso...", "", SharedLog.FileName.Date, SharedLog.LogType.Message);

            SumHoursDevelopment sumHoursDevelopment = new SumHoursDevelopment();

            decimal d = sumHoursDevelopment.SumHoursDevelopmentOfTopics(lstTopics);

            sharedLog.WriteLog($@"Horas desenvolvimento: {d}.", "", SharedLog.FileName.Date, SharedLog.LogType.Message);






        }
    }
}
