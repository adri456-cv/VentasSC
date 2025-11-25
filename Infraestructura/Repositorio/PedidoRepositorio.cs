using Ventas.Core.Interfaces;
using Ventas.Infraestructura.Data;
using Ventas.Core.DTOs;
using Ventas.Core.Mapeadores;
using Microsoft.EntityFrameworkCore;
using Ventas.Core.Entidades;
namespace Ventas.Infraestructura.Repositorio
{
    public class PedidoRepositorio:IPedidoRepositorio
    {
        private readonly VentasContext _context;
        public PedidoRepositorio(VentasContext context)
        {
            this._context = context;
        }
        public async Task<List<PedidoDistribucionDTO>> GetPedidosTodos()
        {
                var consulta = await (
                    from pedido in _context.Pedido
                    join cliente in _context.Cliente
                        on pedido.CodigoCliente equals cliente.Codigo
                    select new PedidoDistribucionDTO
                    {
                        CodigoPedido = pedido.Codigo,
                        Direccion = cliente.Direccion,
                        FechaPedido = pedido.FechaPedido,
                        EstadoPedido = pedido.EstadoPedido
                    }
                    ).ToListAsync(); // O .ToList() si no usas async

                return consulta;
        }
        public async Task<List<PedidoDistribucionDTO>> GetPedidoEntregado()
        {

            var consulta = await (
                    from pedido in _context.Pedido
                    where pedido.EstadoPedido == "entregado"
                    join cliente in _context.Cliente
                        on pedido.CodigoCliente equals cliente.Codigo
                        
                    select new PedidoDistribucionDTO
                    {
                        CodigoPedido = pedido.Codigo,
                        Direccion = cliente.Direccion,
                        FechaPedido = pedido.FechaPedido,
                        EstadoPedido = pedido.EstadoPedido
                    }
                    ).ToListAsync(); 

            return consulta;
        }
        public async Task<List<PedidoDistribucionDTO>> GetEnProceso()
        {
            var consulta = await (
                    from pedido in _context.Pedido
                    where pedido.EstadoPedido == "en proceso"
                    join cliente in _context.Cliente
                        on pedido.CodigoCliente equals cliente.Codigo

                    select new PedidoDistribucionDTO
                    {
                        CodigoPedido = pedido.Codigo,
                        Direccion = cliente.Direccion,
                        FechaPedido = pedido.FechaPedido,
                        EstadoPedido = pedido.EstadoPedido
                    }
                    ).ToListAsync();

            return consulta;
        }
        public async Task<List<PedidoDistribucionDTO>> GetCancelados()
        {
            var consulta = await (
                    from pedido in _context.Pedido
                    where pedido.EstadoPedido == "cancelado"
                    join cliente in _context.Cliente
                        on pedido.CodigoCliente equals cliente.Codigo

                    select new PedidoDistribucionDTO
                    {
                        CodigoPedido = pedido.Codigo,
                        Direccion = cliente.Direccion,
                        FechaPedido = pedido.FechaPedido,
                        EstadoPedido = pedido.EstadoPedido
                    }
                    ).ToListAsync();

            return consulta;
        }
        public async Task<PedidoDTO> GetPedido(string codigo)
        {
            var pedido = await (from u in _context.Pedido
                                where u.Codigo == codigo
                                select u).FirstOrDefaultAsync();
            if (pedido == null)
            {
                return null;
            }
            return PedidoMapeador.toPedidoDTO(pedido);
        }
        public async Task<Pedido> ActualizarEstado(string codigo, string estadoNuevo)
        {
            var estado = await (from u in _context.Pedido
                                where u.Codigo == codigo
                                select u).FirstOrDefaultAsync();
            if (estado == null)
            {
                return null;
            }
            estado.EstadoPedido = estadoNuevo;
            _context.Pedido.Update(estado);
            await _context.SaveChangesAsync();
            return estado;
        }
        public async Task<Pedido> AgregarPedido(Pedido nuevoPedido)
        {
            _context.Pedido.Add(nuevoPedido);
            await _context.SaveChangesAsync();
            return nuevoPedido;
        }

        public async Task<Pedido> EliminarPedido(string codigo)
        {
            var pedido = await (from u in _context.Pedido
                                where u.Codigo == codigo
                                select u).FirstOrDefaultAsync();
            if (pedido == null)
            {
                return null;
            }
            else
            {
                pedido.EstadoPedido = "Cancelado";
                await _context.SaveChangesAsync();
                return pedido;
            }

        }
        public async Task<List<PedidoDetalleDTO>> GetPedidosConDetalles(string codigoP)
        {
            var detalles = await (
                from d in _context.DetallePedido
                where d.CodigoPedido == codigoP
                      && d.Estado == "activo" 
                select new PedidoDetalleDTO
                {
                    CodigoProducto = d.CodigoProducto,
                    Cantidad = d.Cantidad,
                    PrecioUnitarioVenta = d.PrecioUnitarioVenta,
                    Subtotal = d.Cantidad * d.PrecioUnitarioVenta
                }
            ).ToListAsync();

            return detalles;
        }



    }
}
