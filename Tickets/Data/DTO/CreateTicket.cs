using System.ComponentModel.DataAnnotations;
using Tickets.Models;

namespace Tickets.Data.DTO
{
    public class CreateTicket
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0.")]
        public int Quantidade { get; set; }          
        public DateTime DataEntrega { get; set; }
        [Required]
        public int FuncionarioId { get; set; }
    }
}
