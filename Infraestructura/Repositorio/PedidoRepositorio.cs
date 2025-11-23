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

        public async Task<List<PedidoDTO>> GetPedido()
        {
            return await (from u in _context.Pedido
                          where u.EstadoPedido=="Entregado"
                          select u).Select(us=>us.toPedidoDTO()).ToListAsync();

        }
        public async Task<List<PedidoDTO>> GetEnProceso()
        {
            return await (from u in _context.Pedido
                          where u.EstadoPedido == "En Proceso"
                          select u).Select(us => us.toPedidoDTO()).ToListAsync();
        }
        public async Task<List<PedidoDTO>> GetCancelados()
        {
            return await (from u in _context.Pedido
                          where u.EstadoPedido == "Cancelado"
                          select u).Select(us => us.toPedidoDTO()).ToListAsync();
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
                      && d.Estado == "Activo" 
                select new PedidoDetalleDTO
                {
                    CodigoProducto = d.CodigoProducto,
                    Cantidad = d.Cantidad,
                    PrecioUnitarioVenta = d.PrecioUnitarioVenta
                }
            ).ToListAsync();

            return detalles;
        }



    }
}
