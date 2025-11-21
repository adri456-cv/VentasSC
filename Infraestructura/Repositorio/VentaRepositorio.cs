using Ventas.Core.Interfaces;
using Ventas.Infraestructura.Data;
using Ventas.Core.DTOs;
using Ventas.Core.Mapeadores;
using Microsoft.EntityFrameworkCore;
using Ventas.Core.Entidades;
namespace Ventas.Infraestructura.Repositorio
{
    public class VentaRepositorio:IVentaRepositorio
    {
        private readonly VentasContext _context;
        public VentaRepositorio(VentasContext context)
        {
            this._context = context;
        }
        public async Task<List<VentaDTO>> Getventas()
        {
            // 1. Ejecutar la consulta en la DB para obtener List<Venta>
            var ventas = await _context.Venta
                .Where(u => u.EstadoVenta == "Registrada")
                .ToListAsync(); // <-- Ejecuta la consulta aquí

            // 2. Mapear la lista de entidades a una lista de DTO en memoria
            return ventas.Select(v => v.toVentaDTO()).ToList();
        }
        public async Task<VentaDTO>GetVenta(string codigo)
        {
            var encontrado = await (from u in _context.Venta
                                    where u.CodigoVenta == codigo
                                    select u).FirstOrDefaultAsync();
            if (encontrado == null)
            {
                return null;
            }
            else
            {
                return encontrado.toVentaDTO();
            }


        }
        public async Task<Venta> ActualizarEstado(string codigo, string nuevoEstado)
        {
            var estado = await (from u in _context.Venta
                                where u.CodigoVenta == codigo
                                select u).FirstOrDefaultAsync();
            if (estado == null)
            {
                return null;
            }
            estado.EstadoVenta = nuevoEstado;
            _context.Venta.Update(estado);
            await _context.SaveChangesAsync();
            return estado;
        }
        public async Task<Venta> AgregarVenta(Venta nuevaVenta)
        {
            _context.Venta.Add(nuevaVenta);
            await _context.SaveChangesAsync();
            return nuevaVenta;
        }
        public async Task<Venta> EliminarVenta(string codigo)
        {
            var rut = await (from u in _context.Venta
                             where u.CodigoVenta == codigo
                             select u).FirstOrDefaultAsync();
            if (rut == null)
            {
                return null;
            }
            rut.EstadoVenta = "Anulado";
            _context.Venta.Update(rut);
            await _context.SaveChangesAsync();
            return rut;
        }
    }
}
