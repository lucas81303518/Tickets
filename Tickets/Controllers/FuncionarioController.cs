using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Tickets.Data.DTO;
using Tickets.Models;
using Tickets.Services;
using static Tickets.Models.ErrorMessages;

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
            var retorno = await _funcionarioService.AdicionarFuncionario(createFuncionarioDto);
            if (retorno.Success) 
                return Ok(retorno.ErrorMessage);
            if (retorno.ErrorCode == ErrorCode.ErroAoSalvar)
                return Problem(retorno.ErrorMessage);
            return BadRequest(retorno.ErrorMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarFuncionario(int id, [FromBody] UpdateFuncionarioDto updateFuncionarioDto)
        {          
            var retorno = await _funcionarioService.EditarFuncionario(id, updateFuncionarioDto);          
            if (retorno.Success)
                return NoContent();
            if (retorno.ErrorCode == ErrorCode.ErroAoSalvar)
                return Problem(retorno.ErrorMessage);
            if (retorno.ErrorCode == ErrorCode.FuncionarioNaoEncontrado)
                return NotFound(retorno.ErrorMessage);
            return BadRequest(retorno.ErrorMessage);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> RecuperarFuncionarios()            
        {
            var funcionarios = await _funcionarioService.RecuperarFuncionarios();            
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> RecuperarFuncionario(int id)
        {            
            var retorno = await _funcionarioService.RecuperarFuncionario(id);
            if (retorno.Success)
                return Ok(retorno.Data);
            if (retorno.ErrorCode == ErrorCode.ErroAoConsultar)
                return Problem(retorno.ErrorMessage);
            return NotFound(retorno.ErrorMessage);
        }
    }
}
