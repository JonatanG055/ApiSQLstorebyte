using ApiSQLstorebyte.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiSQLstorebyte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ProductosContext _context;

        public CartController(ProductosContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public IActionResult GetCart(int userId)
        {
            var carrito = _context.Carrito.FirstOrDefault(c => c.UsuarioID == userId);
            if (carrito == null)
            {
                return NotFound();
            }

            return Ok(carrito);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Carrito carrito)
        {
            _context.Carrito.Add(carrito);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCart), new { userId = carrito.UsuarioID }, carrito);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Carrito carrito)
        {
            var existingCart = _context.Carrito.FirstOrDefault(c => c.CarritoID == id);
            if (existingCart == null)
            {
                return NotFound();
            }
            existingCart.UsuarioID = carrito.UsuarioID;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var carrito = _context.Carrito.FirstOrDefault(c => c.CarritoID == id);
            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carrito.Remove(carrito);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
