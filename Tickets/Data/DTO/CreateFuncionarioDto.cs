using System.ComponentModel.DataAnnotations;
using Tickets.Models;

namespace Tickets.Data.DTO
{
    public class CreateFuncionarioDto
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [Cpf]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter exatamente 11 dígitos.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter apenas números.")]
        public string Cpf { get; set; }
    }
}
