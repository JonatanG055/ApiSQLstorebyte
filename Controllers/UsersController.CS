using ApiSQLstorebyte.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSQLstorebyte.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ProductosContext _context;

        public UsersController(ProductosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }
        //http://localhost:5045/api/Users
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioID == id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            return Ok(usuario);
        }

        //http://localhost:5045/api/Users

       [HttpPost]
public async Task<IActionResult> Post([FromBody] Usuario usuario)
{
    try
    {
        // Verificar si el usuario ya existe
        var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);
        if (existingUser != null)
        {
            return BadRequest(new { message = "El correo electrónico ya está registrado." });
        }

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = usuario.UsuarioID }, new
        {
            message = "El usuario se creó correctamente",
            data = usuario
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "Ocurrió un error al crear el usuario", error = ex.Message });
    }
}

        //http://localhost:5045/api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Usuario usuario)
        {
            try
            {
                var existingUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioID == id);
                if (existingUser == null)
                {
                    return NotFound(new { message = "Usuario no encontrado" });
                }

                existingUser.Nombre = usuario.Nombre;
                existingUser.Email = usuario.Email;
                existingUser.Password = usuario.Password;

                await _context.SaveChangesAsync();

                return Ok(new { message = "El usuario se editó correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al editar el usuario", error = ex.Message });
            }
        }
        //http://localhost:5045/api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioID == id);
                if (usuario == null)
                {
                    return NotFound(new { message = "Usuario no encontrado" });
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return Ok(new { message = "El usuario se eliminó correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar el usuario", error = ex.Message });
            }
        }


        [HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
{
    try
    {
        var user = await _context.Usuarios.FirstOrDefaultAsync(u => 
            u.Email == loginRequest.Email && u.Password == loginRequest.Password);

        if (user == null)
        {
            return Unauthorized(new { message = "Credenciales incorrectas" });
        }

        return Ok(new { 
            message = "Inicio de sesión exitoso", 
            user = new { user.UsuarioID, user.Nombre, user.Email }
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "Ocurrió un error al iniciar sesión", error = ex.Message });
    }
}


    }
}


