using Ventas.Core.DTOs;
using Ventas.Core.Entidades;
namespace Ventas.Core.Interfaces
{
    public interface IPedidoRepositorio
    {
        Task<List<PedidoDTO>> GetPedido();
        Task<PedidoDTO> GetPedido(string codigo);
        Task<Pedido> ActualizarEstado(string codigo, string estadoNuevo);
        Task<Pedido> AgregarPedido(Pedido nuevoPedido);
        Task<Pedido> EliminarPedido(string codigo);
        Task<List<PedidoDetalleDTO>> GetPedidosConDetalles(string codigoP);

    }
}
