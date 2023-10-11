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
            try
            {
                List<Asesores> asesores = await service.ObtenerAsesores(1);
                return Ok(asesores);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{codigoAsesor}")]
        public async Task<ActionResult<Asesores>> Get(string codigoAsesor)
        {
            try
            {
                var result = await service.ObtenerAsesor(codigoAsesor);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Asesores asesor)
        {
            try
            {
                await service.InsertarAsesor(asesor);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("{codigoAsesor}")]
        public async Task<ActionResult> Put(string codigoAsesor, [FromBody] Asesores asesor)
        {
            try
            {
                if (codigoAsesor != asesor.Asesor)
                {
                    return BadRequest();
                }
                await service.ActualizarAsesor(asesor);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{codigoAsesor}")]
        public async Task<ActionResult> Delete(string codigoAsesor)
        {
            try
            {
                await service.EliminarAsesor(codigoAsesor);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }
    }
}
