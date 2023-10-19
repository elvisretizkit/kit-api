using kit_api.Models;
using kit_api.Security;
using kit_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kit_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly UsuarioService usuarioService = new UsuarioService();
        private IManejadorJwt _Manejador;

        public LoginController(IManejadorJwt manejador) {

            _Manejador = manejador;
        }

        // POST api/<LoginController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] Credenciales credenciales)
        {
            try
            {

                var result = await usuarioService.Login(credenciales.Usuario, credenciales.Password);
                var token = _Manejador.GenerarToken(result.Usuario, result.Tipo);

                return token;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

    }
}
