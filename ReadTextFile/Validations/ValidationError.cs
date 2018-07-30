using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Validations
{
    public class ValidationError
         : IDisposable
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ValidationError(string message)
        {
            Code = 0;
            Message = message;
        }
        public ValidationError(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
