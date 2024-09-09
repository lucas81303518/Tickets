using System.ComponentModel.DataAnnotations;

namespace Tickets.Models
{
    public class TicketEntregue
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0.")]
        public int Quantidade { get; set; }
        [Required]
        [RegularExpression(@"[AI]", ErrorMessage = "O campo Situação deve ser 'A' para Ativo ou 'I' para Inativo.")]
        public char Situacao { get; set; }      
        public DateTime DataEntrega { get; set; }
        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }
    }
}
