using System.ComponentModel.DataAnnotations;

namespace Tickets.Data.DTO
{
    public class ReadTicket
    {
        public int Id { get; set; }   
        public int Quantidade { get; set; }        
        public char Situacao { get; set; }
        public DateTime DataEntrega { get; set; }
    }
}
