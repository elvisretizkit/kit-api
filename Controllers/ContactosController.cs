using kit_api.Models;
using kit_api.Services;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kit_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        readonly ContactoService service = new ContactoService();

        // GET: api/<ContactosController>
        [HttpGet]
        public async Task<ActionResult<List<Contactos>>> Get(int codigoCliente, int codigoContacto, int activo)
        {
            try
            {
                List<Contactos> contactos = await service.ObtenerContactos(codigoCliente, codigoContacto, activo);
                return Ok(contactos);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }



        // GET api/<ContactosController>/5
        [HttpGet("{codigoCliente},{codigoContacto}")]
        public async Task<ActionResult<Contactos>> Get(int codigoCliente, int codigoContacto)
        {
            try
            {
                var result = await service.ObtenerContacto(codigoCliente, codigoContacto,1);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        // POST api/<ContactosController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Contactos contacto)
        {
            try
            {
                await service.InsertarContacto(contacto);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        // PUT api/<ContactosController>/5
        [HttpPut("{codigoCliente},{codigoContacto}")]
        public async Task<ActionResult> Put(int codigoCliente, int codigoContacto, [FromBody] Contactos contacto)
        {
            try
            {
                if (codigoCliente != contacto.Cliente & codigoContacto != contacto.Contacto)
                {
                    return BadRequest();
                }
                await service.ActualizarContacto(contacto);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        // DELETE api/<ContactosController>/5
        [HttpDelete("{codigoCliente},{codigoContacto}")]
        public async Task<ActionResult> Delete(int codigoCliente, int codigoContacto)
        {
            try
            {
                await service.EliminarContacto(codigoCliente, codigoContacto);
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
