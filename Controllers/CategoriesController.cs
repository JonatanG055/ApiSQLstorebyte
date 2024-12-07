using ApiSQLstorebyte.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // Para usar ToListAsync()

namespace ApiSQLstorebyte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ProductosContext _context;

        public CategoriesController(ProductosContext context)
        {
            _context = context;
        }

        // Método GET - Obtener todas las categorías
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return Ok(categorias);
        }

        // Método GET - Obtener una categoría por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada" });
            }

            return Ok(categoria);
        }

        // Método POST - Crear una nueva categoría
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = categoria.CategoriaID }, categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Método PUT - Actualizar una categoría existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return BadRequest(new { message = "El ID de la categoría no coincide con el ID proporcionado en la URL" });
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Categoría actualizada correctamente" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound(new { message = "Categoría no encontrada" });
                }
                else
                {
                    throw;
                }
            }
        }

        // Método DELETE - Eliminar una categoría
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada" });
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Categoría eliminada correctamente" });
        }

        // Método auxiliar para verificar si una categoría existe
        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.CategoriaID == id);
        }
    }
}
