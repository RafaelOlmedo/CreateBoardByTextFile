using System;
using System.IO;
using System.Text;

namespace ReadTextFile.Log
{
    public class SharedLog
    {
        private string _logFileName;
        private readonly string _baseName = Environment.CurrentDirectory + "\\Logs\\";
        private readonly string _fileExtension = ".txt";

        public struct LogType
        {
            public const string Message = "[i] MESSAGE";
            public const string Warning = "[!] WARNING";
            public const string Error = "[x] ERROR";
        }

        public struct FileName
        {
            public const string Default = "log";
            public const string Date = "log_AAAAMMDD";
            public const string Datetime = "log_AAAAMMDD-HHMM";
        }

        public void setLogAAAAMMDDHHMM()
        {
            var now = DateTime.Now;
            var sDay = $"{now.Day:d2}";
            var sMonth = $"{now.Month:d2}";
            var sYear = $"{now.Year:d2}";
            var sHour = $"{now.Hour:d2}";
            var sMinute = $"{now.Minute:d2}";
            _logFileName = "log_" + sYear + sMonth + sDay + "-" + sHour + sMinute + ".txt";
        }
        public void setLogAAAAMMDD()
        {
            var now = DateTime.Now;
            var sDay = $"{now.Day:d2}";
            var sMonth = $"{now.Month:d2}";
            var sYear = $"{now.Year:d2}";
            _logFileName = "log_" + sYear + sMonth + sDay + ".txt";
        }
        public string getLogAAAAMMDDHHMM()
        {
            return _logFileName;
        }

        /// <summary>
        /// Write a log line on the file
        /// </summary>
        public void WriteLog(string logLine, string path, string fileName, string logType)
        {
            try
            {
                // Name of the log file after validation
                //string finalFileName = string.Empty;
                //Verify if the folder exists
                if (!new DirectoryInfo(_baseName).Exists)
                    Directory.CreateDirectory(_baseName);
                if (path != string.Empty)
                {
                    if (!new DirectoryInfo(path).Exists)
                        Directory.CreateDirectory(path);
                }
                //Verify the name of file log
                if (fileName == "log_AAAAMMDD")
                {
                    setLogAAAAMMDD();
                    fileName = _logFileName;
                }
                else if (fileName == "log_AAAAMMDD-HHMM")
                {
                    setLogAAAAMMDDHHMM();
                    fileName = _logFileName;
                }
                else if (fileName == "log")
                    fileName = "log" + _fileExtension;  
                using (var sw = new StreamWriter(_baseName + fileName, true, Encoding.Default))
                {
                    sw.WriteLine($"{logType};{DateTime.Now};{logLine}");
                    sw.Close();
                }
            }
            catch
            {
                // ignored
            }
        }
        public void WriteDetailedLog(string errMsg, Exception exception)
        {
            try
            {
                if (errMsg == string.Empty)
                    errMsg = "Erro ao realizar processo";
                WriteLog(errMsg + ";" + exception.Message + ";" + exception.StackTrace.Replace("\r\n", "") + ";" + exception.TargetSite.Name + ";" + exception.Source, "", SharedLog.FileName.Default, SharedLog.LogType.Error);
            }
            catch
            {
                // ignored
            }
        }
        #region IDisposable Members
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}