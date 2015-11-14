using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Grid
    {
        public int Id { get; set; }
        public String Slug { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }
        public int Width { get; set; }
        public bool FixedWidth { get; set; }
        public Item[][] Items { get; set; }

        public String PageTitle { get; set; }
        public String PageDescription { get; set; }
        public String PageKeywords { get; set; }

        public bool HasPermission { get; set; }
    }

    public class PartialGrid : IValidatableObject
    {
        public int Id { get; set; }
        public String Slug { get; set; }
        public String Title { get; set; }
        public String Text { get; set; }
        public int Width { get; set; }
        public bool FixedWidth { get; set; }

        public String PageTitle { get; set; }
        public String PageDescription { get; set; }
        public String PageKeywords { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (Title.Length >= 255)
                errors.Add(new ValidationResult("Превышена длина заголовка"));
            if (Text.Length >= 8000)
                errors.Add(new ValidationResult("Превышена длина текста"));
            if (Slug.Length >= 255)
                errors.Add(new ValidationResult("Превышена длянна ссылки"));
            if (!Regex.IsMatch(@"^[a-zA-Z]+$", Slug))
                errors.Add(new ValidationResult("Ссылка содержит не допустимые символы"));
            if (Width < 0 || Width > 20)
                errors.Add(new ValidationResult("Недопустимая ширина листа"));
            return errors;
        }
    }
}
