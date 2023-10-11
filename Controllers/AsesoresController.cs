using Microsoft.AspNetCore.Mvc;
using kit_api.Models;
using kit_api.Services;

namespace kit_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsesoresController : ControllerBase
    {
        readonly AsesorService service = new AsesorService();

        [HttpGet]
        public async Task <ActionResult<List<Asesores>>> Get()
        {
            List<Asesores> asesores = await service.ObtenerAsesores(1);
            return asesores;
        }

        [HttpGet("{codigoAsesor}")]
        public async Task<ActionResult<Asesores>> Get(string codigoAsesor)
        {
            var result = await service.ObtenerAsesor(codigoAsesor);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Asesores asesor)
        {
            await service.InsertarAsesor(asesor);
            return NoContent();
        }

        [HttpPut("{codigoAsesor}")]
        public async Task<ActionResult> Put(string codigoAsesor, [FromBody] Asesores asesor)
        {
            if (codigoAsesor != asesor.Asesor)
            {
                return BadRequest();
            }
            await service.ActualizarAsesor(asesor);
            return NoContent();
        }

        [HttpDelete("{codigoAsesor}")]
        public async Task<ActionResult> Delete(string codigoAsesor)
        {
            await service.EliminarAsesor(codigoAsesor);
            return NoContent();
        }
    }
}
