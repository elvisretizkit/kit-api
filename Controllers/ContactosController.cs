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
        [HttpPut]
        public async Task<ActionResult> Put([FromQuery] ContactosQueryParams parametros, [FromBody] Contactos contacto)
        {
            try
            {
                if (parametros.codigoCliente != contacto.Cliente & parametros.codigoContacto != contacto.Contacto)
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
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] ContactosQueryParams parametros)
        {
            try
            {
                await service.EliminarContacto(parametros.codigoCliente, parametros.codigoContacto);
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
