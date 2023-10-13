using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace kit_api.Models
{
    public class ContactosQueryParams
    {
        [BindRequired]
        public int codigoCliente {  get; set; }
        public int codigoContacto { get; set; }
        public int activo { get; set; }
    }
}
