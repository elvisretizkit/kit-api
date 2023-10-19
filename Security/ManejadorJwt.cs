using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace kit_api.Security
{
    public class ManejadorJwt : IManejadorJwt
    {
        public IConfiguration Configuration { get; set; }

        public ManejadorJwt(IConfiguration configuration) {
            Configuration = configuration;
        }

        public string GenerarToken(string usuario, int tipo) {
            var claims = new[] {
             new Claim("usuario", usuario),
             new Claim("tipo", tipo.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JWT:Key").Get<string>() ?? string.Empty));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokey = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials
                ) ;
            return new JwtSecurityTokenHandler().WriteToken(tokey);
           ;
        }
    }
}
