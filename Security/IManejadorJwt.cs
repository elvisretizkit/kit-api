namespace kit_api.Security
{
    public interface IManejadorJwt
    {
        public string GenerarToken(string usuario, int tipo);
        public string GenerarRefreshToken();
    }
}
