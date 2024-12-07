namespace ApiSQLstorebyte.Models
{
    public class Carrito
    {
        public int CarritoID { get; set; }
        public int UsuarioID { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
