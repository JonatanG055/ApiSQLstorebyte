namespace ApiSQLstorebyte.Models
{
    public class Producto
    {
        public int Id { get; set; }  // Cambiado a Id
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaID { get; set; }

        // Nueva propiedad para la URL de la imagen del producto
        public string? ImagenUrl { get; set; }

        public Categoria? Categoria { get; set; }
    }
}

