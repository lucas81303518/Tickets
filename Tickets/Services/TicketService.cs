using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tickets.Data;
using Tickets.Data.DTO;
using Tickets.Models;

namespace Tickets.Services
{
    public class TicketService
    {
        private readonly TicketsContext _context;
        private readonly IMapper _mapper;
        public TicketService(TicketsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AdicionarTicket(CreateTicket createTicket)
        {
            var ticketModel = _mapper.Map<TicketEntregue>(createTicket);
            await _context.AddAsync(ticketModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditarTicket(int id, UpdateTicket updateTicket)
        {
            var ticket = await _context.TicketsEntregue.FirstOrDefaultAsync(t=> t.Id == id);
            if (ticket == null)
                throw new KeyNotFoundException($"Ticket id: {id} não existe!");
            try
            {
                _mapper.Map(updateTicket, ticket);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Erro ao salvar as alterações {ex.Message}");
            }
        }

        public async Task<TicketsResumo> RecuperarTicketsDetalhado
            (DateTime dataInicial, DateTime dataFinal, char situacao, int idFuncionario)
        {
            var query = _context.TicketsEntregue
                        .Where(t => t.DataEntrega >= dataInicial && t.DataEntrega <= dataFinal);

            if ((situacao.ToString() != "\0") && (situacao != 'T'))
                query = query.Where(t => t.Situacao == situacao);          

            if (idFuncionario > 0)   
                query = query.Where(t => t.FuncionarioId == idFuncionario);

            var ticketsData = await query
                .Select(t => new
                {
                    t.Id,
                    t.Funcionario,
                    t.Quantidade,
                    t.Situacao,
                    t.DataEntrega
                })
                .ToListAsync();

            var totalGeralTickets = ticketsData.Sum(t => t.Quantidade);

            var detalhesPorFuncionario = ticketsData
                .GroupBy(t => t.Funcionario)
                .Select(g => new ReadTicketsDetalhado
                {
                    Funcionario = g.Key,
                    TotalTicketsPorFuncionario = g.Sum(t => t.Quantidade),
                    Tickets = g.Select(t => new ReadTicket
                    {   Id = t.Id,
                        Situacao = t.Situacao,
                        DataEntrega = t.DataEntrega,                      
                        Quantidade = t.Quantidade                        
                    }).ToList() 
                }).ToList();

            var resultado = new TicketsResumo
            {
                TotalGeralTickets = totalGeralTickets,
                DetalhesPorFuncionario = detalhesPorFuncionario
            };

            return resultado;
        }
    }
}
