using kit_api.Models;
using kit_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace kit_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        readonly GuardiaService serviceGuardia = new GuardiaService();
        readonly ContactoService serviceContacto = new ContactoService();
        readonly ClienteService service = new ClienteService();

        // GET: api/<ClientesController>
        [HttpGet]
        public async Task<ActionResult<List<Clientes>>> Get()
        {
            try
            {
                List<Clientes> clientes = await service.ObtenerClientes();
                return Ok(clientes);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }


        // GET api/<ClientesController>/5
        [HttpGet("{codigoCliente}")]
        public async Task<ActionResult<Clientes>> Get(string codigoCliente)
        {
            try
            {
                var result = await service.ObtenerCliente(codigoCliente);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }


        // GET api/<ClientesController>/Guardias
        [HttpGet("Guardias/{codigoCliente}")]
        public async Task<ActionResult<List<Guardias>>> GetGuardias(int codigoCliente)
        {
            try
            {
                List<Guardias> guardias = await serviceGuardia.ObtenerGuardiasDeCliente(codigoCliente);
                return Ok(guardias);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }


        // GET: api/<ClientesController>/Contactos
        [HttpGet("Contactos")]
        public async Task<ActionResult<List<Contactos>>> Get([FromQuery] ContactosQueryParams parametros)
        {
            try
            {
                List<Contactos> contactos = await serviceContacto.ObtenerContactos(parametros.codigoCliente, parametros.codigoContacto, parametros.activo);
                return Ok(contactos);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }


        // POST api/<ClientesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Clientes cliente)
        {
            try
            {
                await service.InsertarCliente(cliente);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }


        // PUT api/<ClientesController>/5
        [HttpPut("{codigoCliente}")]
        public async Task<ActionResult> Put(int codigoCliente, [FromBody] Clientes cliente)
        {
            try
            {
                if (codigoCliente != cliente.Cliente)
                {
                    return BadRequest();
                }
                await service.ActualizarCliente(cliente);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }


        // DELETE api/<ClientesController>/5
        [HttpDelete("{codigoCliente}")]
        public async Task<ActionResult> Delete(int codigoCliente)
        {
            try
            {
                await service.EliminarCliente(codigoCliente);
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
