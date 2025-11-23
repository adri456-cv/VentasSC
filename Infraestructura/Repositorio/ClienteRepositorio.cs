using Ventas.Core.Interfaces;
using Ventas.Infraestructura.Data;
using Ventas.Core.DTOs;
using Ventas.Core.Mapeadores;
using Microsoft.EntityFrameworkCore;
using Ventas.Core.Entidades;
namespace Ventas.Infraestructura.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly VentasContext _context;
        public ClienteRepositorio(VentasContext context)
        {
            this._context = context;
        }
        //Nueva base
        public async Task<List<ClienteDTO>> GetListaCliente()
        {
            return await (from u in _context.Cliente
                              where u.Estado=="Alta"
                              select u).Select(us=>us.toClienteDTO()).ToListAsync();
        }
        public async Task<ClienteDTO>GetCliente(string ci)
        {
            var cliente = await (from u in _context.Cliente
                                 where u.Codigo == ci
                                 select u).FirstOrDefaultAsync();
            if (cliente == null)
            {
                return null;
            }
            return ClienteMapeador.toClienteDTO(cliente);
        }
        public async Task<Cliente> AgregarCliente(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
        public async Task<ClienteDTO> ActualizarCliente(string ci, ClienteDTO clienteActualizado)
        {
            var cliente = await (from u in _context.Cliente
                                 where u.Codigo == ci
                                 select u).FirstOrDefaultAsync();
            if (cliente == null)
            {
                return null;
            }
            cliente.Codigo = clienteActualizado.Codigo;
            cliente.Nombre = clienteActualizado.Nombre;
            cliente.Apellido = clienteActualizado.Apellido;
            cliente.Telefono = clienteActualizado.Telefono;
            cliente.Contacto = clienteActualizado.Contacto;
            cliente.Direccion = clienteActualizado.Direccion;
            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();
            return ClienteMapeador.toClienteDTO(cliente);
        }
        public async Task<Cliente> EliminarCliente(string ci)
        {
            var cliente = await(from u in _context.Cliente
                                where u.Codigo == ci
                                select u ).FirstOrDefaultAsync();
            if (cliente == null)
            {
                return null;
            }
            cliente.Estado = "Baja";
            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;

        }


    }
}
