using Ventas.Core.DTOs;
using Ventas.Core.Entidades;

namespace Ventas.Core.Interfaces
{
    public interface IVentaRepositorio
    {
        Task<List<VentaDTO>>Getventas();
        Task<VentaDTO> GetVenta(string codigo);
        Task<Venta> ActualizarEstado(string codigo, string nuevoEstado);
        Task<Venta> AgregarVenta(Venta nuevaVenta);
        Task<Venta> EliminarVenta(string codigo);
    }
}
