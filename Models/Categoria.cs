namespace ApiSQLstorebyte.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
