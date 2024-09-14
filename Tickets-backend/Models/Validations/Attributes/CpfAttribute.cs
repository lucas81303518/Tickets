using System.ComponentModel.DataAnnotations;
using Tickets.Models.ValidationsAttribute;

namespace Tickets.Models
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {           
            var validationResult = CpfValidator.Validate(value as string);

            if (validationResult.Success)
            {
                return ValidationResult.Success;
            }
            
            return new ValidationResult(validationResult.ErrorMessage);
        }
    }
}
