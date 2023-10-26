using kit_api.Models;
using kit_api.Security;
using kit_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                var refreshToken = _Manejador.GenerarRefreshToken(result.Usuario);
                await usuarioService.InsertarRefreshToken(refreshToken);


                return new { token, refreshToken = refreshToken.Token };
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpPost("Refresh")]
        public async Task<ActionResult<object>> PostRefresh([FromBody] RefreshBodySchema schema)
        {
            try
            {
                //var manejadorToken = new JwtSecurityTokenHandler();
                
                var refresToken = await usuarioService.ObtenerRefreshToken(schema.RefreshToken);
                if (refresToken.Activo == false || refresToken.Expiracion <= DateTime.Now) { 
                    return Unauthorized();
                }
                

                /*TODO:Manejar mas adelante si el token ya esta usado
                  TODO:Manejar validacion accestoken

                var accesTokenValidate = await manejadorToken.ValidateTokenAsync(schema.AccesToken,new TokenValidationParameters() {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Manejador.Configuration.GetSection("JWT:Key").Get<string>() ?? string.Empty)),
                    ClockSkew = System.TimeSpan.Zero
                });

                if (accesTokenValidate.IsValid == false) {
                    return Unauthorized();
                }
                */

                refresToken.Usado = true;

                //TODO:Implementar actualizacion de estado usado
                
                var usuario = await usuarioService.ObtenerUsuario(refresToken.Usuario);
                if (usuario.Activo == false) {
                    return Unauthorized();
                }

                //TODO:Revisar si Login es necesario
                //var result = await usuarioService.Login(usuario.Usuario, usuario.Password);
                
                var token = _Manejador.GenerarToken(usuario.Usuario, usuario.Tipo);
                var nuevoRefreshToken = _Manejador.GenerarRefreshToken(usuario.Usuario);
                await usuarioService.InsertarRefreshToken(nuevoRefreshToken);
                var refreshTokenString = nuevoRefreshToken.Token;

                return new
                {
                    token,
                    refresToken = refreshTokenString
                };


            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }
    }
}
