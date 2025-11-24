using Microsoft.CodeAnalysis.CSharp.Syntax;
using Ventas.Core.DTOs;
using Ventas.Core.Entidades;

namespace Ventas.Core.Mapeadores
{
    public static class VentaMapeador
    {
        public static VentaDTO toVentaDTO(this Venta venta)
        {
            return new VentaDTO()
            {
                CodigoPedido = venta.CodigoPedido,
                CodigoCliente = venta.CodigoCliente,
                CodigoSucursal = venta.CodigoSucursal,
                CodigoVenta = venta.CodigoVenta,
                FechaEmision = venta.FechaEmision,
                MontoTotal = venta.MontoTotal

            };
        }
        public static Venta toVenta(this VentaDTO venta)
        {
            return new Venta()
            {
                CodigoPedido = venta.CodigoPedido,
                CodigoCliente = venta.CodigoCliente,
                CodigoSucursal = venta.CodigoSucursal,
                CodigoVenta = venta.CodigoVenta,
                FechaEmision = venta.FechaEmision,
                MontoTotal = venta.MontoTotal,
                EstadoVenta = "registrada"

            };
        }
    }
}
