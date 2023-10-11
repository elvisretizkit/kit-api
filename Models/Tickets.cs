namespace kit_api.Models
{
    public class Tickets
    {
        public int Ticket { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Tema { get; set; }
        public int Estatus { get; set; }
        public int Cliente { get; set; }
        public int Contacto { get; set; }
        public int Prioridad { get; set; }
        public string Asesor { get; set; }
        public string Categoria { get; set; }
        public float Tiempo { get; set; }
        public string Descripcion { get; set; }
        public bool Facturable { get; set; }
        public DateTime FechaProgramado { get; set; }
    }
}
