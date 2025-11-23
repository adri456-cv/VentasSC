using Ventas.Core.DTOs;
using Ventas.Core.Entidades;
namespace Ventas.Core.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<List<ClienteDTO>> GetListaCliente();
        Task<ClienteDTO> GetCliente(string ci);
        Task<Cliente> AgregarCliente(Cliente nuevoCliente);
        Task<ClienteDTO> ActualizarCliente(string ci, ClienteDTO clienteActualizado);
        Task<Cliente> EliminarCliente(string ci);

    }
}
