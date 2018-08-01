using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadTextFile.Validations
{
    public class ValidationResult
        : IDisposable
    {
        private readonly List<ValidationError> _errors;

        public string Message { get; set; }
        public bool IsValid
        {
            get { return _errors.Count == 0; }
        }
        public IEnumerable<ValidationError> Errors
        {
            get { return _errors.AsEnumerable(); }
        }

        public ValidationResult()
        {
            _errors = new List<ValidationError>();
        }

        public void Add(ValidationError error)
        {
            _errors.Add(error);
        }
        public void Add(string message)
        {
            _errors.Add(new ValidationError(_errors.Count() + 1, message));
        }
        public void Add(int code, string message)
        {
            _errors.Add(new ValidationError(code, message));
        }

        public void Dispose()
        {
            _errors.ForEach(q => q.Dispose());
            GC.SuppressFinalize(this);
        }
    }
}
