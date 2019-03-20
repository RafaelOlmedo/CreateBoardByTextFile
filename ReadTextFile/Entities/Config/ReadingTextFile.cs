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
                if (string.IsNullOrEmpty(FilePath))
                {
                    AddNotification(nameof(FilePath), "Caminho do arquivo da estimativa não informado.");
                    return this;
                }

                if (!System.IO.Directory.Exists(FilePath))
                    AddNotification(nameof(FilePath), $@"Caminho do arquivo informado não existe. Caminho: {this.FilePath}. ");

            }
            catch (Exception ex)
            {
                AddNotification("Exception", $@"Erro ao validar caminho do arquivo no arquivo de configuração. Retorno: {ex.Message}.");
            }

            return this;
        }

        public ReadingTextFile validIfFileExists()
        {
            try
            {
                if (string.IsNullOrEmpty(this.FileName))
                {
                    AddNotification(nameof(FileName), "Arquivo de estimativa não informado.");
                    return this;
                }

                if (!System.IO.File.Exists(FilePath + FileName))
                    AddNotification("FilePath / FileName", $@"Arquivo informado não existe. Arquivo: {this.FilePath + this.FileName}. ");

            }
            catch (Exception ex)
            {
                AddNotification("Exception", $@"Erro ao validar nome do arquivo no arquivo de configuração. Retorno: {ex.Message}.");
            }

            return this;
        }

        public void validate()
        {
            validIfFilePathExists();
            validIfFileExists();
        }
    }
}
