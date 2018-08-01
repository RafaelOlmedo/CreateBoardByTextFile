using ReadTextFile.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFile.Entities.Base
{
    public class BaseEntity
    {
        public ValidationResult validationResults { get; set; }

        public BaseEntity()
        {
            validationResults = new ValidationResult();
        }
    }
}
