using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace kit_api.Models
{
    public class TiemposQueryParams
    {
        [BindRequired]
        public int codigoTicket{ get; set; }
        [BindRequired] 
        public int codigoFolio { get; set; }
    }
}



