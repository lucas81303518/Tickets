using System.ComponentModel.DataAnnotations;
using Tickets.Models;

namespace Tickets.Data.DTO
{
    public class CreateTicket
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0.")]
        public int Quantidade { get; set; }                  
        [Required]
        public ICollection<int> idFuncionarios { get; set; }
    }
}
