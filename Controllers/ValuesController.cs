using ApiSQLstorebyte.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")] // Ruta base: /api/values
public class ValuesController : ControllerBase
{
    private readonly ProductosContext _context;

    public ValuesController(ProductosContext context)
    {
        _context = context;
    }

    // GET: api/values
    [HttpGet]
    public IActionResult Get()
    {
        var values = new[] { "Value1", "Value2", "Value3" };
        return Ok(values); // Devuelve un 200 con la lista de valores
    }

    // GET: api/values/{id}
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var producto = _context.Productos.FirstOrDefault(p => p.Id == id); // Usando 'Id' en lugar de 'ProductoID'
        if (producto == null)
        {
            return NotFound(); // Si no se encuentra el producto, devuelve un 404
        }
        return Ok(producto); // Devuelve el producto encontrado
    }

    // POST: api/values
    [HttpPost]
    public IActionResult Post([FromBody] Producto producto)
    {
        if (producto == null)
        {
            return BadRequest("Producto no puede ser nulo");
        }

        // Aquí agregas lógica para guardar el producto en la base de datos
        _context.Productos.Add(producto);
        _context.SaveChanges();

        return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto); // Usando 'Id' en lugar de 'ProductoID'
    }

    // PUT: api/values/{id}
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Producto producto)
    {
        var existingProduct = _context.Productos.FirstOrDefault(p => p.Id == id); // Buscando el producto por Id
        if (existingProduct == null)
        {
            return NotFound(); // Si no se encuentra el producto, devuelve un 404
        }

        // Actualiza el producto
        existingProduct.Nombre = producto.Nombre;
        existingProduct.Precio = producto.Precio;
        existingProduct.Stock = producto.Stock;
        existingProduct.CategoriaID = producto.CategoriaID;

        _context.SaveChanges();

        return NoContent(); // Devuelve un 204 indicando que la operación fue exitosa
    }

    // DELETE: api/values/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var producto = _context.Productos.FirstOrDefault(p => p.Id == id); // Buscando el producto por Id
        if (producto == null)
        {
            return NotFound(); // Si no se encuentra el producto, devuelve un 404
        }

        _context.Productos.Remove(producto);
        _context.SaveChanges();

        return NoContent(); // Devuelve un 204 indicando que el recurso fue eliminado
    }
}
