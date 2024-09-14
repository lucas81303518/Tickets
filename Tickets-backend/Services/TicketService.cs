using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tickets.Data;
using Tickets.Data.DTO;
using Tickets.Models;
using static Tickets.Models.ErrorMessages;

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

        private bool IsErrorFK_Funcionario(Exception ex)
        {
            return ex.InnerException != null && ex.InnerException.Message.Contains("FK_TicketsEntregue_Funcionarios_FuncionarioId");
        }

        public async Task<ResponseClient<object>> AdicionarTicket(CreateTicket createTicket)
        {
            try
            {
                foreach (int funcionarioId in createTicket.idFuncionarios)
                {
                    var ticketModel = _mapper.Map<TicketEntregue>(createTicket);
                    ticketModel.Situacao = 'A';
                    ticketModel.DataEntrega = DateTime.Now;
                    ticketModel.FuncionarioId = funcionarioId;
                    await _context.AddAsync(ticketModel);
                }
                
                await _context.SaveChangesAsync();
                return new ResponseClient<object>(true, ErrorCode.OperacaoBemSucedida);
            }
            catch (Exception ex)
            {
                if (IsErrorFK_Funcionario(ex))
                {
                    return new ResponseClient<object>(false, ErrorCode.FuncionarioNaoEncontrado);
                }
                return new ResponseClient<object>(false, ErrorCode.ErroAoSalvar, ex.Message);
            }            
        }

        public async Task<ResponseClient<object>> EditarTicket(int id, UpdateTicket updateTicket)
        {
            var ticket = await _context.TicketsEntregue.FirstOrDefaultAsync(t=> t.Id == id);
            if (ticket == null)
                return new ResponseClient<object>(false, ErrorCode.TicketNaoEncontrado);
            try
            {
                _mapper.Map(updateTicket, ticket);
                await _context.SaveChangesAsync();
                return new ResponseClient<object>(true, ErrorCode.OperacaoBemSucedida);
            }       
            catch (Exception ex)
            {
                if (IsErrorFK_Funcionario(ex))
                {
                    return new ResponseClient<object>(false, ErrorCode.FuncionarioNaoEncontrado);
                }
                return new ResponseClient<object>(false, ErrorCode.ErroAoSalvar, ex.Message);
            }     
        }

        public async Task<ResponseClient<IEnumerable<TicketEntregue>>> RecuperarTickets()
        {
            try
            {
                var tickets = await _context.TicketsEntregue.ToListAsync();
                return new ResponseClient<IEnumerable<TicketEntregue>>(true, tickets);
            }
            catch (Exception ex)
            {
                return new ResponseClient<IEnumerable<TicketEntregue>>(false, ErrorCode.ErroAoConsultar, ex.Message);
            }
        }

        public async Task<ResponseClient<TicketEntregue>> RecuperarTicket(int id)
        {
            try
            {
                var ticket = await _context.TicketsEntregue.FirstOrDefaultAsync(t => t.Id == id);
                if (ticket == null)
                    return new ResponseClient<TicketEntregue>(false, ErrorCode.TicketNaoEncontrado);

                return new ResponseClient<TicketEntregue>(true, ticket);
            }
            catch (Exception ex)
            {
                return new ResponseClient<TicketEntregue>(false, ErrorCode.ErroAoConsultar, ex.Message);
            }
        }

        public async Task<TicketsResumo> RecuperarTicketsDetalhado
            (DateTime dataInicial, DateTime dataFinal, char situacao, int idFuncionario)
        {
            var query = _context.TicketsEntregue
                        .Where(t => t.DataEntrega.Date >= dataInicial && t.DataEntrega.Date <= dataFinal);

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
