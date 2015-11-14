using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IValidatableObject
    {
        public String Login { get; set; }
        public String Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (Login.Length >= 255)
                errors.Add(new ValidationResult("Long Login"));
            if (Password.Length >= 255)
                errors.Add(new ValidationResult("Long password"));
            return errors;
        }
    }
}
