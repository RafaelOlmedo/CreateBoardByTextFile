﻿using Newtonsoft.Json;
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
        // TODO - Private set.

        public string FilePath { get; set; }
        public string FileName { get; set; }

        //public Validations.ValidationResult validationResults { get; set; }

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
                configProperties.validationResults.Add($@"Erro ao recuparar informações de configuração. Retorno {ex.Message}.");
            }

            return configProperties;
        }

        public ConfigProperties validIfFilePathExists()
        {
            // TODO - Validar se validar dessa maneira está correto.
            try
            {
                if (string.IsNullOrEmpty(this.FilePath))
                    this.validationResults.Add("Caminho do arquivo não informado.");

                if (!this.validationResults.IsValid)
                    return this;

                if (!System.IO.Directory.Exists(this.FilePath))
                    this.validationResults.Add($@"Caminho do arquivo informado não existe. Caminho: {this.FilePath}. ");

            }
            catch (Exception ex)
            {
                this.validationResults.Add($@"Erro ao validar caminho do arquivo no arquivo de configuração. Retorno: {ex.Message}.");
            }

            return this;
        }

        public ConfigProperties validIfFileExists(ConfigProperties configProperties)
        {
            // TODO - Validar se validar dessa maneira está correto.
            try
            {
                if (string.IsNullOrEmpty(configProperties.FileName))
                    configProperties.validationResults.Add("Arquivo não informado.");

                if (!configProperties.validationResults.IsValid)
                    return configProperties;

                if (!System.IO.File.Exists(configProperties.FilePath + configProperties.FileName))
                    configProperties.validationResults.Add($@"Arquivo informado não existe. Arquivo: {configProperties.FilePath + configProperties.FileName}. ");

            }
            catch (Exception ex)
            {
                configProperties.validationResults.Add($@"Erro ao validar nome do arquivo no arquivo de configuração. Retorno: {ex.Message}.");
            }

            return configProperties;
        }

    }
}
