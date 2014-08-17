using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestMvc.Models
{
    public class ThingViewModel : IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        public int Size { get; set; }

        public bool IsEdited { get; set; }

        public string Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (((Name ?? "") + (Body ?? "")).Count() < 6)
            {
                yield return new ValidationResult("Combined must be greater than 6!");
            }
        }
    }
}