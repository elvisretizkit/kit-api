using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace kit_api.Models
{
    public class RefreshBodySchema
    {
        [BindRequired]
        public string AccesToken { get; set; }
        [BindRequired]
        public string RefresToken { get; set; }
    }
}
