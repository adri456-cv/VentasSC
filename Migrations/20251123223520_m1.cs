using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ventas.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Apellido = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    Contacto = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "DetallePedido",
                columns: table => new
                {
                    IdDetallePedido = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    CodigoPedido = table.Column<string>(type: "text", nullable: false),
                    CodigoProducto = table.Column<string>(type: "text", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    PrecioUnitarioVenta = table.Column<double>(type: "double precision", nullable: false),
                    SubTotal = table.Column<double>(type: "double precision", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedido", x => x.IdDetallePedido);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    IdPedido = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    CodigoCliente = table.Column<string>(type: "text", nullable: false),
                    CodigoEmpleado = table.Column<string>(type: "text", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstadoPedido = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.IdPedido);
                });

            migrationBuilder.CreateTable(
                name: "Ruta",
                columns: table => new
                {
                    IdRuta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoRuta = table.Column<string>(type: "text", nullable: false),
                    CodigoCliente = table.Column<string>(type: "text", nullable: false),
                    CodigoPedido = table.Column<string>(type: "text", nullable: false),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    Orden = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruta", x => x.IdRuta);
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    IdVenta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoPedido = table.Column<string>(type: "text", nullable: false),
                    CodigoCliente = table.Column<string>(type: "text", nullable: false),
                    CodigoSucursal = table.Column<string>(type: "text", nullable: false),
                    CodigoVenta = table.Column<string>(type: "text", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MontoTotal = table.Column<double>(type: "double precision", nullable: false),
                    EstadoVenta = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.IdVenta);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "DetallePedido");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Ruta");

            migrationBuilder.DropTable(
                name: "Venta");
        }
    }
}
