using System.ComponentModel.DataAnnotations;
using Tickets.Models;

namespace Tickets.Data.DTO
{
    public class UpdateFuncionarioDto
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        [Cpf]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter exatamente 11 dígitos.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter apenas números.")]
        public string Cpf { get; set; }

        [Required]
        [RegularExpression(@"[AI]", ErrorMessage = "O campo Situação deve ser 'A' para Ativo ou 'I' para Inativo.")]
        public char Situacao { get; set; }
    }
}
