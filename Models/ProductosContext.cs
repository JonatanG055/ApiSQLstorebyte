using Microsoft.EntityFrameworkCore;
using ApiSQLstorebyte.Models;

namespace ApiSQLstorebyte.Models
{
    public class ProductosContext : DbContext
    {
        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DetalleOrden> DetallesOrden { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de precisión y escala para los decimales
            modelBuilder.Entity<DetalleOrden>()
                .Property(d => d.PrecioUnitario)
                .HasColumnType("decimal(18,2)");  // Especifica la precisión y escala

            modelBuilder.Entity<Orden>()
                .Property(o => o.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");
        }
    }
}
