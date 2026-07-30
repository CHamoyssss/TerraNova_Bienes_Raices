using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddLatitudLongitud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitud",
                table: "Propiedades",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitud",
                table: "Propiedades",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitud",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Longitud",
                table: "Propiedades");
        }
    }
}
