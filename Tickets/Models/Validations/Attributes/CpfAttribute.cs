using System.ComponentModel.DataAnnotations;
using Tickets.Models.ValidationsAttribute;

namespace Tickets.Models
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cpf = value as string;

            if (CpfValidator.IsValid(cpf))
                return ValidationResult.Success;

            return new ValidationResult("O CPF informado é inválido.");
        }
    }
}
