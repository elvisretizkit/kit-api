using kit_api.Models;

namespace kit_api.Security
{
    public interface IManejadorJwt
    {
        public string GenerarToken(string usuario, int tipo);
        public Refresh GenerarRefreshToken(string usuario);

        public IConfiguration Configuration { get; set; }
    }
}
