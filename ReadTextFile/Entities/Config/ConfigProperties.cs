using Newtonsoft.Json;
using ReadTextFile.Entities.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Entities.Config
{
    public class ConfigProperties : BaseEntity
    {        
        public ReadingTextFile ReadingTextFile { get; set; }
        public CreateBoard CreateBoard { get; set; }        

        public ConfigProperties() 
        {
            //validationResults = new Validations.ValidationResult();
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
                    configProperties.validationResults.Add("Arquivo de configuração não encontrado.");

                if(configProperties.validationResults.IsValid)
                {
                    // Realiza a leitura do arquivo.
                    StreamReader r = new StreamReader(ConfigPath);
                    string json = r.ReadToEnd();

                    // Deserializa o JSON para o objeto.
                    configProperties = JsonConvert.DeserializeObject<ConfigProperties>(json);
                }               

            }
            catch (Exception ex)
            {
                this.validationResults.Add($@"Erro ao recuparar informações de configuração. Retorno {ex.Message}.");
            }

            return configProperties;
        }     

    }
}
