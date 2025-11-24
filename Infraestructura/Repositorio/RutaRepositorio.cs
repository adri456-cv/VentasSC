using Ventas.Core.Interfaces;
using Ventas.Infraestructura.Data;
using Ventas.Core.DTOs;
using Ventas.Core.Mapeadores;
using Microsoft.EntityFrameworkCore;
using Ventas.Core.Entidades;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Ventas.Infraestructura.Repositorio
{
    
    public class RutaRepositorio:IRutaRepositorio
    {
        private readonly VentasContext _context;

        public RutaRepositorio(VentasContext context)
        {
            this._context = context;
        }
        public async Task<List<RutaDTO>> GetRutas()
        {
            return await (from u in _context.Ruta
                          where u.Estado == "Activo"
                          select u).Select(us => us.toRutaDTO()).ToListAsync();
        }
        public async Task<RutaDTO> GetRuta(string codigo)
        {
            var encontrado = await (from u in _context.Ruta
                                    where u.CodigoRuta == codigo
                                    select u).FirstOrDefaultAsync();
            if (encontrado == null)
            {
                return null;
            }
            else
            {
                return encontrado.toRutaDTO();
            }
        }
        public async Task<List<ListaRutaDTO>> GetRutasPorDia()
        {
            var lista = await (
                from r in _context.Ruta

                join p in _context.Pedido
                    on r.CodigoPedido equals p.Codigo into pedidos
                from p in pedidos.DefaultIfEmpty()   // <-- mantiene rutas aunque no haya pedido

                join c in _context.Cliente
                    on p.CodigoCliente equals c.Codigo into clientes
                from c in clientes.DefaultIfEmpty()  // <-- mantiene rutas aunque no haya cliente

                where p != null
                      && c != null
                      && p.CodigoCliente == r.CodigoCliente    // tu validación

                orderby Convert.ToInt32(r.Orden)

                select new ListaRutaDTO
                {
                    CodigoRuta=r.CodigoRuta,
                    CodigoPedido = r.CodigoPedido,
                    Direccion = c.Direccion,
                    FechaCreacion=r.FechaCreacion,
                    Orden = r.Orden
                }
            ).ToListAsync();

            return lista;
        }

        public async Task<List<PedidoRutaDTO>> GetPedidos(string codigo)
        {
            var lista = await (
                from u in _context.Ruta
                join p in _context.Pedido
                    on u.CodigoPedido equals p.Codigo
                where u.CodigoRuta == codigo
                select new PedidoRutaDTO
                {
                    CodigoPedido = p.Codigo,
                    CodigoCliente = p.CodigoCliente,
                    EstadoPedido = p.EstadoPedido,
                    MontoTotal = _context.DetallePedido
                        .Where(dp => dp.CodigoPedido == p.Codigo && dp.Estado.ToLower() == "activo")
                        .Sum(dp => (double?)dp.Cantidad * dp.PrecioUnitarioVenta) ?? 0
                }
            ).ToListAsync();

            return lista;
        }



        public async Task<Ruta> AgregarRuta(Ruta nuevaRuta)
        {
            _context.Ruta.Add(nuevaRuta);
            await _context.SaveChangesAsync();
            return nuevaRuta;
        }
        public async Task<RutaDTO> ActualizarRuta(string codigo, RutaDTO rutaDto)
        {
            var ruta = await (from u in _context.Ruta
                                 where u.CodigoRuta == codigo
                                 select u).FirstOrDefaultAsync();
            if (ruta == null)
            {
                return null;
            }
            ruta.CodigoRuta = rutaDto.CodigoRuta;
            ruta.CodigoCliente = rutaDto.CodigoCliente;
            
            ruta.Orden = rutaDto.Orden;
            
            _context.Ruta.Update(ruta);
            await _context.SaveChangesAsync();
            return RutaMapeador.toRutaDTO(ruta);
        }
        public async Task<Ruta> EliminarRuta(string codigo)
        {
            var rut = await (from u in _context.Ruta
                                 where u.CodigoRuta == codigo
                                 select u).FirstOrDefaultAsync();
            if (rut == null)
            {
                return null;
            }
            rut.Estado = "Cancelado";
            _context.Ruta.Update(rut);
            await _context.SaveChangesAsync();
            return rut;
        }
    }
}
