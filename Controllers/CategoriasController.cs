using kit_api.Models;
using kit_api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kit_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        // GET: api/<CategoriasController>
        readonly CategoriaService service = new CategoriaService();

        [HttpGet]
        public async Task<ActionResult<List<Categorias>>> Get()
        {
            try
            {
                List<Categorias> categorias = await service.ObtenerCategorias();
                return Ok(categorias);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        // GET api/<CategoriasController>/5
        [HttpGet("{codigoCategoria}")]
        public async Task<ActionResult<Categorias>> Get(string codigoCategoria)
        {
            try
            {
                var result = await service.ObtenerCategoria(codigoCategoria);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        // POST api/<CategoriasController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Categorias categoria)
        {
            try
            {
                await service.InsertarCategoria(categoria);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{codigoCategoria}")]
        public async Task<ActionResult> Put(string codigoCategoria, [FromBody] Categorias categoria)
        {
            try
            {
                if (codigoCategoria != categoria.Categoria)
                {
                    return BadRequest();
                }
                await service.ActualizarCategoria(categoria);
                return NoContent();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return StatusCode(500);
            }
        }

        // DELETE api/<CategoriasController>/5
        [HttpDelete("{codigoCategoria}")]
        public async Task<ActionResult> Delete(string codigoCategoria)
        {
            try
            {
                await service.EliminarCategoria(codigoCategoria);
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
