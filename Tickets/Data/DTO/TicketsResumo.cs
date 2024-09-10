namespace Tickets.Data.DTO
{
    public class TicketsResumo
    {
        public int TotalGeralTickets { get; set; } 
        public virtual ICollection<ReadTicketsDetalhado> DetalhesPorFuncionario { get; set; }
    }

}
