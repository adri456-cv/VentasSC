using Ventas.Core.DTOs;
using Ventas.Core.Entidades;
namespace Ventas.Core.Mapeadores
{
    public static class PedidoMapeador
    {
        public static PedidoDTO toPedidoDTO(this Pedido pedido)
        {
            return new PedidoDTO()
            {
                Codigo = pedido.Codigo,
                CodigoCliente = pedido.CodigoCliente,
                CodigoEmpleado = pedido.CodigoEmpleado,
                FechaPedido = pedido.FechaPedido,
                
            };
        }
        public static Pedido toPedido(this PedidoDTO dto)
        {
            return new Pedido()
            {
                Codigo = dto.Codigo,
                CodigoCliente = dto.CodigoCliente,
                CodigoEmpleado = dto.CodigoEmpleado,
                FechaPedido = dto.FechaPedido,
                EstadoPedido = "en Proceso"

            };
        }
    }
}
