namespace ApiSQLstorebyte.Models
{
    public class DetalleOrden
    {
        public int DetalleOrdenID { get; set; }
        public int OrdenID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public Orden? Orden { get; set; }
        public Producto? Producto { get; set; }
    }
}
