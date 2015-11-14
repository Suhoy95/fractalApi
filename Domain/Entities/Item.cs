using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Item : IValidatableObject
    {
        public int id { get; set; }
        public int gridId { get; set; }
        public String type { get; set; }
        public int[] analogy { get; set; }
        public int[] sup { get; set; }
        public int[] sub { get; set; }

        public String slug { get; set; }

        public String title { get; set; }
        public String text { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            switch (type)
            {
                case "note":
                    NoteValidate(errors);
                    break;
                case "gridItem":
                    GridItemvalidate(errors);
                    break;
                default:
                    errors.Add(new ValidationResult("Недопустимый тип"));
                    break;
            }
            return errors;
        }

        private void NoteValidate(List<ValidationResult> errors)
        {
            if (title.Length >= 255)
                errors.Add(new ValidationResult("Превышена длина заголовка"));
            if (text.Length >= 8000)
                errors.Add(new ValidationResult("Превышена длина текста"));
        }

        private void GridItemvalidate(List<ValidationResult> errors)
        {
            if (title.Length >= 255)
                errors.Add(new ValidationResult("Превышена длина заголовка"));
            if (text.Length >= 8000)
                errors.Add(new ValidationResult("Превышена длина текста"));
            if (slug.Length >= 255)
                errors.Add(new ValidationResult("Превышена длянна ссылки"));
            if (!Regex.IsMatch(slug, @"^[a-zA-Z]{0,255}$"))
                errors.Add(new ValidationResult("Ссылка содержит не допустимые символы"));
        }
    }
}
