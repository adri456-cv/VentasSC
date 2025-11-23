using Ventas.Core.DTOs;
using Ventas.Core.Entidades;
namespace Ventas.Core.Interfaces
{
    public interface IRutaRepositorio
    {
        Task<List<RutaDTO>> GetRutas();
        Task<RutaDTO> GetRuta(string codigo);
        Task<Ruta> AgregarRuta(Ruta nuevaRuta);
        Task<RutaDTO> ActualizarRuta(string codigo, RutaDTO rutaDto);
        Task<Ruta> EliminarRuta(string codigo);
        Task<List<ListaRutaDTO>> GetRutasPorDia();
    }
}
