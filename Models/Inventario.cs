namespace ApiSQLstorebyte.Models
{
    public class Inventario
    {
        public int InventarioID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }

        public Producto? Producto { get; set; }
    }
}
