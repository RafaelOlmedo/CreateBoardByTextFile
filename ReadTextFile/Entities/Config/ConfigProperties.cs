using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Entities.Config
{
    public class ConfigProperties
    {
        // TODO - Private set.

        public string FilePath{ get; set; }
        public string FileName { get; set; }

        public ConfigProperties()
        {

        }

        /// <summary>
        /// Realiza a leitura do arquivo json de configuração e retorna objeto com as informações.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public ConfigProperties readJsonConfigFile()
        {
            ConfigProperties configProperties = new ConfigProperties();

            try
            {
                // Caminho do arquivo de configuração.
                // TODO - Alterar para de alguma forma pegar do caminho do projeto.
                string ConfigPath = Environment.CurrentDirectory + @"\Config\ConfigProperties.json";

                if (!System.IO.File.Exists(ConfigPath))
                    return configProperties;

                // Realiza a leitura do arquivo.
                StreamReader r = new StreamReader(ConfigPath);
                string json = r.ReadToEnd();

                // Deserializa o JSON para o objeto.
                configProperties = JsonConvert.DeserializeObject<ConfigProperties>(json);

            }
            catch(Exception ex)
            {
                // Incluir tratamento de log.
            }
           

            return configProperties;
        }

    }
}
