using ReadTextFile.Entities.Base;
using System;

namespace ReadTextFile.Entities.Config
{
    public class ReadingTextFile : BaseEntity
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public ReadingTextFile validIfFilePathExists()
        {
            try
            {
                if (string.IsNullOrEmpty(this.FilePath))
                {
                    this.validationResults.Add("Caminho do arquivo da estimativa não informado.");
                    return this;
                }

                if (!System.IO.Directory.Exists(this.FilePath))
                    this.validationResults.Add($@"Caminho do arquivo informado não existe. Caminho: {this.FilePath}. ");

            }
            catch (Exception ex)
            {
                this.validationResults.Add($@"Erro ao validar caminho do arquivo no arquivo de configuração. Retorno: {ex.Message}.");
            }

            return this;
        }

        public ReadingTextFile validIfFileExists()
        {
            try
            {
                if (string.IsNullOrEmpty(this.FileName))
                {
                    this.validationResults.Add("Arquivo de estimativa não informado.");
                    return this;
                }

                if (!System.IO.File.Exists(this.FilePath + this.FileName))
                    this.validationResults.Add($@"Arquivo informado não existe. Arquivo: {this.FilePath + this.FileName}. ");

            }
            catch (Exception ex)
            {
                this.validationResults.Add($@"Erro ao validar nome do arquivo no arquivo de configuração. Retorno: {ex.Message}.");
            }

            return this;
        }

        public ReadingTextFile validate()
        {
            validIfFilePathExists();
            validIfFileExists();

            return this;
        }
    }
}
