using kit_api.Models;
using kit_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kit_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        readonly TicketService serviceTicket = new TicketService();
        readonly TiempoService serviceTiempo = new TiempoService();

        [HttpGet("{codigoTicket}")]
        public async Task<ActionResult<Tickets>> Get(int codigoTicket)
        {
            try
            {
                var result = await serviceTicket.ObtenerTicket(codigoTicket);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        // POST: api/<TicketsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Tickets ticket)
        {
            try
            {
                await serviceTicket.InsertarTicket(ticket);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }


        // POST: api/<TicketsController>/Tiempos
        [HttpPost("Tiempos")]
        public async Task<ActionResult> Post([FromBody] Tiempos tiempo)
        {
            try
            {
                await serviceTiempo.InsertarTiempo(tiempo);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }



        // PUT api/<TicketsController>/5
        [HttpPut("{codigoTicket}")]
        public async Task<ActionResult> Put(int codigoTicket, [FromBody] Tickets ticket)
        {
            try
            {
                if (codigoTicket != ticket.Ticket)
                {
                    return BadRequest();
                }
                await serviceTicket.ActualizarTicket(ticket);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }


        // PUT api/<TicketsController>/Tiempos
        [HttpPut("Tiempos")]
        public async Task<ActionResult> Put(int codigoTicket, int codigoFolio,[FromBody] Tiempos tiempo)
        {
            try
            {
                await serviceTiempo.ActualizarTiempo(tiempo);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }


        // DELETE api/<TicketsController>/5
        [HttpDelete("{codigoTicket}")]
        public async Task<ActionResult> Delete(int codigoTicket)
        {
            try
            {
                await serviceTicket.EliminarTicket(codigoTicket);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }


        // DELETE api/<TicketsController>/Tiempos/
        [HttpDelete("Tiempos")]
        public async Task<ActionResult> Delete([FromQuery] TiemposQueryParams parametros)
        {
            try
            {
                await serviceTiempo.EliminarTiempo(parametros.codigoTicket, parametros.codigoFolio);
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
