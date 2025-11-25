using Ventas.Core.DTOs;
using Ventas.Core.Entidades;
namespace Ventas.Core.Interfaces
{
    public interface IPedidoRepositorio
    {
        Task<List<PedidoDistribucionDTO>> GetPedidoEntregado();
        Task<List<PedidoDistribucionDTO>> GetEnProceso();
        Task<List<PedidoDistribucionDTO>> GetCancelados();
        Task<List<PedidoDistribucionDTO>> GetPedidosTodos();
        Task<PedidoDTO> GetPedido(string codigo);
        Task<Pedido> ActualizarEstadoEntregado(string codigo);
        Task<Pedido> ActualizarEstadoCancelado(string codigo);
        Task<Pedido> AgregarPedido(Pedido nuevoPedido);
        Task<Pedido> EliminarPedido(string codigo);
        Task<List<PedidoDetalleDTO>> GetPedidosConDetalles(string codigoP);

    }
}
