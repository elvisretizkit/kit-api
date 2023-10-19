using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace kit_api.Models
{
    public class Credenciales
    {
        [BindRequired]
        public string Usuario { get; set; }
        [BindRequired]
        public string Password { get; set; }
    }
}
