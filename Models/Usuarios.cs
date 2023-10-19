namespace kit_api.Models
{
    public class Usuarios
    {
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public string Clave { get; set; }
        public string Password { get; set; }
        public int Cliente { get; set; }
        public int Contacto { get; set; }
        public string Asesor { get; set; }
        public bool Activo { get; set; }

    }
}
