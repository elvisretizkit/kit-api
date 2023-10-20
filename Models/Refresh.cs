namespace kit_api.Models
{
    public class Refresh
    {
        public string Token {  get; set; }
        public DateTime Expiracion { get; set; }
        public bool Activo { get; set; }
        public bool Usado { get; set; }
        public string Usuario { get; set; }

    }
}
