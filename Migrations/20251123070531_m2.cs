using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ventas.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoTotalPedido",
                table: "Pedido");

            migrationBuilder.AddColumn<string>(
                name: "CodigoPedido",
                table: "Ruta",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaCreacion",
                table: "Ruta",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoPedido",
                table: "Ruta");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Ruta");

            migrationBuilder.AddColumn<double>(
                name: "MontoTotalPedido",
                table: "Pedido",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
