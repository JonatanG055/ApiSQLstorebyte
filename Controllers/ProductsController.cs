using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSQLstorebyte.Models;

namespace ApiSQLstorebyte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductosContext _context;

        public ProductsController(ProductosContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }

            return Ok(new
            {
                message = "Producto encontrado",
                data = producto
            });
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, new
            {
                message = "El producto se creó correctamente",
                data = producto
            });
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest(new { message = "El ID del producto no coincide con el ID proporcionado en la URL" });
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound(new { message = "El producto no existe y no se pudo actualizar" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "El producto se actualizó correctamente" });
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound(new { message = "El producto no existe y no se puede eliminar" });
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok(new { message = "El producto se eliminó correctamente" });
        }

        // Método auxiliar para verificar si el producto existe
        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }


        // GET: api/Categories
[HttpGet("categories")]
public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
{
    var categorias = await _context.Categorias.ToListAsync();
    return Ok(categorias);
}

    }
}
