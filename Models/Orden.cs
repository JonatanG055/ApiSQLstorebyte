namespace ApiSQLstorebyte.Models
{
    public class Orden
    {
        public int OrdenID { get; set; }
        public int UsuarioID { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
