using ApiSQLstorebyte.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiSQLstorebyte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ProductosContext _context;

        public OrdersController(ProductosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ordenes = _context.Ordenes.ToList();
            return Ok(ordenes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var orden = _context.Ordenes.FirstOrDefault(o => o.OrdenID == id);
            if (orden == null)
            {
                return NotFound();
            }
            return Ok(orden);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Orden orden)
        {
            _context.Ordenes.Add(orden);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = orden.OrdenID }, orden);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Orden orden)
        {
            var existingOrder = _context.Ordenes.FirstOrDefault(o => o.OrdenID == id);
            if (existingOrder == null)
            {
                return NotFound();
            }
            existingOrder.Fecha = orden.Fecha;
            existingOrder.Total = orden.Total;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orden = _context.Ordenes.FirstOrDefault(o => o.OrdenID == id);
            if (orden == null)
            {
                return NotFound();
            }

            _context.Ordenes.Remove(orden);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
