using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Coords : IValidatableObject
    {
        public int GridId { get; set; }
        public int[][] coords { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            
            foreach (var coord in coords)
                if (coord.Length != 3)
                    errors.Add(new ValidationResult("Bad coord"));
            
            return errors;
        }
    }
}
