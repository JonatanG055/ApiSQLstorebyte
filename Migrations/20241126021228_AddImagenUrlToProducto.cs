using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSQLstorebyte.Migrations
{
    /// <inheritdoc />
    public partial class AddImagenUrlToProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                table: "Productos");
        }
    }
}
