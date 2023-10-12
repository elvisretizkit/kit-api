using Microsoft.AspNetCore.Mvc;
using kit_api.Models;
using kit_api.Services;

namespace kit_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardiasController : ControllerBase
    {
        readonly GuardiaService service = new GuardiaService();

        [HttpGet]
        public async Task<ActionResult<List<Asesores>>> Get()
        {
            try
            {
                List<Guardias> guardias = await service.ObtenerGuardias();
                return Ok(guardias);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{codigoGuardia}")]
        public async Task<ActionResult<Guardias>> Get(int codigoGuardia)
        {
            try
            {
                var result = await service.ObtenerGuardia(codigoGuardia);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Guardias guardia)
        {
            try
            {
                await service.InsertarGuardia(guardia);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("{codigoGuardia}")]
        public async Task<ActionResult> Put(int codigoGuardia, [FromBody] Guardias guardia)
        {
            try
            {
                if (codigoGuardia != guardia.Guardia)
                {
                    return BadRequest();
                }
                await service.ActualizarGuardia(guardia);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{codigoGuardia}")]
        public async Task<ActionResult> Delete(int codigoGuardia)
        {
            try
            {
                await service.EliminarGuardia(codigoGuardia);
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