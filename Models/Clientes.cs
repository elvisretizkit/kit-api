namespace kit_api.Models
{
    public class Clientes
    {
        public int Cliente { get; set; }
        public string RazonSocial { get; set; }
        public bool Activo { get; set; }
        public bool PolizaAnual { get; set; }
        public string Contacto { get; set; }
        public string Correo { get; set; }
        public string NombreCorto { get; set; }
        public string NombreCotizacion { get; set; }
        public string Categoria { get; set; } 
        public string Direccion { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        //TODO: Revisar si hay que eliminar esta propiedad
        public  string Version { get; set; }
        public float HorasContrato { get; set; }
        public string Telefono { get; set; }
    }
}
