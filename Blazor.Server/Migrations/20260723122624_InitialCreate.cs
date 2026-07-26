using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CI = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    TipoCliente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Comprador")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clientes__3214EC07E317ACD1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trabajadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CI = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PorcentajeComision = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    FechaContratacion = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Trabajad__3214EC0709AD560B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Propiedades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Zona = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    AreaM2 = table.Column<double>(type: "float", nullable: false),
                    Habitaciones = table.Column<int>(type: "int", nullable: false),
                    Banios = table.Column<int>(type: "int", nullable: false),
                    Garajes = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Disponible"),
                    TipoOperacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Venta"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagenUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IdTrabajador = table.Column<int>(type: "int", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Propieda__3214EC07230E0E80", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Propiedades_Trabajadores",
                        column: x => x.IdTrabajador,
                        principalTable: "Trabajadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContraseñaHash = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Agente"),
                    IdTrabajador = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__3214EC0713F400B2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Trabajadores",
                        column: x => x.IdTrabajador,
                        principalTable: "Trabajadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdPropiedad = table.Column<int>(type: "int", nullable: false),
                    IdTrabajador = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Monto = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    ComisionGenerada = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    FormaPago = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Contado"),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Completada"),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ventas__3214EC071EE7C768", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ventas_Propiedades",
                        column: x => x.IdPropiedad,
                        principalTable: "Propiedades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ventas_Trabajadores",
                        column: x => x.IdTrabajador,
                        principalTable: "Trabajadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Visitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdPropiedad = table.Column<int>(type: "int", nullable: false),
                    IdTrabajador = table.Column<int>(type: "int", nullable: false),
                    FechaVisita = table.Column<DateTime>(type: "datetime", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Programada"),
                    Comentarios = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Visitas__3214EC0711EE2058", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitas_Clientes",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Visitas_Propiedades",
                        column: x => x.IdPropiedad,
                        principalTable: "Propiedades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Visitas_Trabajadores",
                        column: x => x.IdTrabajador,
                        principalTable: "Trabajadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Clientes__32149A7A785F14DC",
                table: "Clientes",
                column: "CI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Clientes__4EC504804484003C",
                table: "Clientes",
                column: "Telefono",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Clientes__A9D10534D9AD21D4",
                table: "Clientes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Propiedades_IdTrabajador",
                table: "Propiedades",
                column: "IdTrabajador");

            migrationBuilder.CreateIndex(
                name: "UQ__Trabajad__32149A7AD280977B",
                table: "Trabajadores",
                column: "CI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Trabajad__A9D10534E566E69C",
                table: "Trabajadores",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdTrabajador",
                table: "Usuarios",
                column: "IdTrabajador");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__6B0F5AE086D65208",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdCliente",
                table: "Ventas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdPropiedad",
                table: "Ventas",
                column: "IdPropiedad");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdTrabajador",
                table: "Ventas",
                column: "IdTrabajador");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdCliente",
                table: "Visitas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdPropiedad",
                table: "Visitas",
                column: "IdPropiedad");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdTrabajador",
                table: "Visitas",
                column: "IdTrabajador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Visitas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Propiedades");

            migrationBuilder.DropTable(
                name: "Trabajadores");
        }
    }
}
