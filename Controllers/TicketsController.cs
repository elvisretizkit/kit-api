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
    }
}
