using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tickets.Data.DTO;
using Tickets.Services;
using static Tickets.Models.ErrorMessages;

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
            var retorno = await _ticketService.AdicionarTicket(createTicket);
            if (retorno.Success)
                return Ok(retorno.ErrorMessage);
            if (retorno.ErrorCode == ErrorCode.ErroAoSalvar)
                return Problem(retorno.ErrorMessage);
            return BadRequest(retorno.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTicket(int id, UpdateTicket updateTicket)
        {            
            var retorno = await _ticketService.EditarTicket(id, updateTicket);            
            if (retorno.Success)
                return NoContent();
            if (retorno.ErrorCode == ErrorCode.ErroAoSalvar)
                return Problem(retorno.ErrorMessage);
            if (retorno.ErrorCode == ErrorCode.TicketNaoEncontrado)
                return NotFound(retorno.ErrorMessage);
            return BadRequest(retorno.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarTickets()
        {
            var retorno = await _ticketService.RecuperarTickets();
            if (retorno.Success)         
                return Ok(retorno.Data);     
            if (retorno.ErrorCode == ErrorCode.ErroAoConsultar)
                return Problem(retorno.ErrorMessage);
            return BadRequest(retorno.ErrorMessage); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarTicket(int id)
        {
            var retorno = await _ticketService.RecuperarTicket(id);
            if (retorno.Success)           
                return Ok(retorno.Data);            
            if (retorno.ErrorCode == ErrorCode.ErroAoConsultar)
                return Problem(retorno.ErrorMessage);
            return NotFound(retorno.ErrorMessage); 
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
