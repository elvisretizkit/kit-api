using kit_api.Models;
using kit_api.Security;
using kit_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kit_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly UsuarioService usuarioService = new UsuarioService();
        private IManejadorJwt _Manejador;
        private iManejadorEncripcion _handler;
        public LoginController(IManejadorJwt manejador, iManejadorEncripcion manejadorEncripcion) {

            _Manejador = manejador;
            _handler = manejadorEncripcion;
        }

        

        // POST api/<LoginController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<object>> Post([FromBody] Credenciales credenciales)
        {
            try
            {
                credenciales.Password = _handler.Encriptar(credenciales.Password);
                var result = await usuarioService.Login(credenciales.Usuario, credenciales.Password);
                var token = _Manejador.GenerarToken(result.Usuario, result.Tipo);
                var refreshToken = _Manejador.GenerarRefreshToken();

                return new { token, refreshToken };
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("Refresh")]
        public async Task<ActionResult<string>> PostRefresh([FromHeader(Name = "Authorization")] string refreshToken)
        {
            try
            {
                var manejadorToken = new JwtSecurityTokenHandler();
                var tokenValidado = manejadorToken.ReadJwtToken(refreshToken);
                string tipo = tokenValidado.Claims.First(c => c.Type == JwtRegisteredClaimNames.NameId).ToString();
                return tipo;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }
    }
}
