using Tickets.Models;

namespace Tickets.Data.DTO
{
    public class ReadTicketsDetalhado
    {        
        public virtual Funcionario Funcionario { get; set; }
        public int TotalTicketsPorFuncionario { get; set; }
        public virtual ICollection<ReadTicket> Tickets { get; set; }        
    }
}
