using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tickets.Data.DTO;
using Tickets.Services;

namespace Tickets.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FuncionarioController: ControllerBase
    {
        private readonly FuncionarioService _funcionarioService;

        public FuncionarioController(FuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarFuncionario([FromBody] CreateFuncionarioDto createFuncionarioDto)
        {
            try
            {
                await _funcionarioService.AdicionarFuncionario(createFuncionarioDto);
            }
            
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }              
            return Created(nameof(RecuperarFuncionario), createFuncionarioDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarFuncionario(int id, [FromBody] UpdateFuncionarioDto updateFuncionarioDto)
        {
            try
            {
                await _funcionarioService.EditarFuncionario(id, updateFuncionarioDto);
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UpdateFuncionarioDto>>> RecuperarFuncionarios()
        {
            var funcionarios = await _funcionarioService.RecuperarFuncionarios();            
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UpdateFuncionarioDto>> RecuperarFuncionario(int id)
        {
            try
            {
                var funcionario = await _funcionarioService.RecuperarFuncionario(id);               
                return Ok(funcionario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
