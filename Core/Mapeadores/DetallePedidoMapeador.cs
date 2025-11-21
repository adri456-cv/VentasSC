using Ventas.Core.DTOs;
using Ventas.Core.Entidades;
namespace Ventas.Core.Mapeadores
{
    public static class DetallePedidoMapeador
    {
        public static DetallePedidoDTO toDetallePedidoDTO(this DetallePedido detalle)
        {
            return new DetallePedidoDTO()
            {
                Codigo = detalle.Codigo,
                CodigoPedido = detalle.CodigoPedido,
                CodigoProducto = detalle.CodigoProducto,
                Cantidad = detalle.Cantidad,
                PrecioUnitarioVenta = detalle.PrecioUnitarioVenta,
            };
        }
        public static DetallePedido toDetallePedido (this DetallePedidoDTO detalleDTO)
        {
            return new DetallePedido()
            {
                Codigo = detalleDTO.Codigo,
                CodigoPedido = detalleDTO.CodigoPedido,
                CodigoProducto = detalleDTO.CodigoProducto,
                Cantidad = detalleDTO.Cantidad,
                PrecioUnitarioVenta = detalleDTO.PrecioUnitarioVenta,
                Estado = "Activo"
            };
        }
    }
}
