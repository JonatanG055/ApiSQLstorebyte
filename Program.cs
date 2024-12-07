using ApiSQLstorebyte.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión a la base de datos en Azure
builder.Services.AddDbContext<ProductosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection"))
);

// Configuración de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar los controladores
builder.Services.AddControllers();

// Configuración de CORS para permitir que el frontend (React) se comunique con el backend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:3000") // Cambiar a la URL de tu frontend
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Verificar la conexión a la base de datos
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductosContext>();
    try
    {
        // Hacer una consulta simple para verificar la conexión
        dbContext.Database.CanConnect();
        Console.WriteLine("Conexión a la base de datos exitosa.");
    }
    catch (Exception ex)
    {
        // Si hay un error, mostrarlo en la consola
        Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
    }
}

// Configuración para el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Agregar la autorización (si es necesario)
app.UseAuthorization();

// Habilitar CORS
app.UseCors("AllowFrontend");

// Mapear los controladores
app.MapControllers();

app.Run();
