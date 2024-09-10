using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tickets.Data.DTO;
using Tickets.Services;

namespace Tickets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;
        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTicket(CreateTicket createTicket)
        {
            try
            {
                await _ticketService.AdicionarTicket(createTicket);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created(nameof(RecuperarTicketsDetalhado), createTicket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTicket(int id, UpdateTicket updateTicket)
        {
            try
            {
                await _ticketService.EditarTicket(id, updateTicket);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpGet("RelatorioDetalhado")]
        public async Task<ActionResult<TicketsResumo>> RecuperarTicketsDetalhado
            ([FromQuery][Required] DateTime dataInicial,
            [FromQuery][Required] DateTime dataFinal,
            [FromQuery] char situacao,
            [FromQuery] int idFuncionario)
        {
            return await _ticketService.RecuperarTicketsDetalhado
                (dataInicial, dataFinal, situacao, idFuncionario);
        }
    }
}
