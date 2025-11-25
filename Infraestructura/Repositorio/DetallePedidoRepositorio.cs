using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Ventas.Core.DTOs;
using Ventas.Core.Entidades;
using Ventas.Core.Interfaces;
using Ventas.Core.Mapeadores;
using Ventas.Infraestructura.Data;

namespace Ventas.Infraestructura.Repositorio
{
    public class DetallePedidoRepositorio : IDetallePedidoRepositorio
    {
        private readonly VentasContext _context;
        public DetallePedidoRepositorio(VentasContext context)
        {
            this._context = context;
        }
        public async Task<List<DetallePedidoDTO>> GetListaDetalle()
        {
            return await (from u in _context.DetallePedido
                          where u.Estado.ToLower() == "activo"
                          select u).Select(us=>us.toDetallePedidoDTO()).ToListAsync();
        }

        public async Task<DetallePedidoDTO> GetDetalle(string codigo)
        {
            var encontrado = await(from u in _context.DetallePedido
                                   where u.Codigo == codigo
                                   select u).FirstOrDefaultAsync();
            if (encontrado == null)
            {
                return null;
            }
            else
            {
                return encontrado.toDetallePedidoDTO();
            }
        }
        public async Task<DetallePedido> ActualizarEstado(string codigo, string estadoNuevo)
        {
            var estado = await (from u in _context.DetallePedido
                                where u.Codigo == codigo
                                select u).FirstOrDefaultAsync(); 
            if (estado == null)
            {
                return null;
            }
            estado.Estado = estadoNuevo;
            _context.DetallePedido.Update(estado);
            await _context.SaveChangesAsync();
            return estado;
        }
        public async Task<DetallePedido> AgregarDetalle(DetallePedido nuevoDetalle)
        {
            _context.DetallePedido.Add(nuevoDetalle);
            await _context.SaveChangesAsync();
            return nuevoDetalle;

        }
        public async Task<DetallePedido> EliminarDetalle(string codigo)
        {
            var eliminado = await (from u in _context.DetallePedido
                                   where u.Codigo == codigo
                                   select u).FirstOrDefaultAsync();
            if(eliminado == null)
            {
                return null;
            }
            else
            {
                eliminado.Estado = "cancelado";
                await _context.SaveChangesAsync();
                return eliminado;
            }
        }
        public async Task<List<ProductoCantidadDTO>> ProductosTotales()
        {
            var productosAgrupados = await _context.DetallePedido
            .Where(dp => dp.Estado.ToLower() == "activo") 
            .GroupBy(dp => dp.CodigoProducto) 
            .Select(g => new ProductoCantidadDTO 
            {
                
                CodigoProducto = g.Key,

                
                Cantidad = g.Sum(dp => dp.Cantidad)
            })
            .OrderByDescending(dto => dto.Cantidad) 
            .ToListAsync();

            return productosAgrupados;

        }
        public async Task<List<ProductoMenosVendidoDTO>> ProductoMenos()
        {
            var lista = await _context.DetallePedido
            .Where(d => d.Estado.ToLower() == "activo")  
            .GroupBy(d => d.CodigoProducto)    
            .Select(g => new ProductoMenosVendidoDTO
            {
                CodigoProducto = g.Key,
                CantidadVendida = g.Sum(x => x.Cantidad) 
            })
            .OrderBy(x => x.CantidadVendida) 
            .ToListAsync();

                return lista;
        }
    }
}
