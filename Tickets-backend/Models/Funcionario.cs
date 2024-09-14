using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Tickets.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]        
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter exatamente 11 dígitos.")]
        [Cpf]
        public string Cpf { get; set; }

        [Required]
        [RegularExpression(@"[AI]", ErrorMessage = "O campo Situação deve ser 'A' para Ativo ou 'I' para Inativo.")]
        public char Situacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
