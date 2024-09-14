using Microsoft.AspNetCore.Mvc;

namespace Tickets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteConexao: ControllerBase
    {
        [HttpGet]
        public IActionResult Teste()
        {
            return Ok("Conexão estabelicida com sucesso!");
        }
    }
}
