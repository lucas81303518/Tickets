using AutoMapper;
using Tickets.Data.DTO;
using Tickets.Models;

namespace Tickets.Profiles
{
    public class TicketProfile: Profile
    {
        public TicketProfile()
        {
            CreateMap<CreateTicket, TicketEntregue>();
            CreateMap<UpdateTicket, TicketEntregue>();
        }
    }
}
