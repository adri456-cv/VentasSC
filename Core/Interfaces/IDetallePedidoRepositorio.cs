using Ventas.Core.DTOs;
using Ventas.Core.Entidades;
namespace Ventas.Core.Interfaces
{
    public interface IDetallePedidoRepositorio
    {
        Task<List<DetallePedidoDTO>>GetListaDetalle();
        Task<DetallePedidoDTO> GetDetalle(string codigo);
        Task<DetallePedido> ActualizarEstado(string codigo, string estadoNuevo);
        Task<DetallePedido> AgregarDetalle(DetallePedido nuevoDetalle);
        Task<DetallePedido> EliminarDetalle(string codigo);
        Task<ProductoCantidadDTO> ProductosTotales(string codigo);
        Task<List<ProductoMenosVendidoDTO>> ProductoMenos();
    }
}
