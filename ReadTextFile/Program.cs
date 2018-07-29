using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReadTextFile.Entities;
using ReadTextFile.Entities.Config;
using ReadTextFile.Services;

namespace ReadTextFile
{
    class Program
    {
        static void Main(string[] args)
        {
            // Recupera configurações necessárias.
            ConfigProperties configProperties =
                new ConfigProperties().readJsonConfigFile();

            if (string.IsNullOrEmpty(configProperties.FilePath) ||
                string.IsNullOrEmpty(configProperties.FileName))
            {
                // TODO - Incluir tratamento.

            }

            List<Topic> lstTopics = ReadFileText.readTextFileToObject(configProperties);

            int i = 0;
        }
    }
}
