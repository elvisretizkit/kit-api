using kit_api.Models;
using kit_api.Security;
using kit_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kit_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        readonly UsuarioService service = new UsuarioService();

        private iManejadorEncripcion _handler;

        public UsuariosController(iManejadorEncripcion handler)
        {
            _handler = handler;
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuarios usuario)
        {
            try
            {
                usuario.Password = _handler.Encriptar(usuario.Password);
                await service.InsertarUsuario(usuario);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }
    }
}
