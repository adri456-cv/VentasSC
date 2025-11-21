using Ventas.Core.Interfaces;
using Ventas.Infraestructura.Data;
using Ventas.Core.DTOs;
using Ventas.Core.Mapeadores;
using Microsoft.EntityFrameworkCore;
using Ventas.Core.Entidades;

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
            ruta.CodigoEmpleado = rutaDto.CodigoEmpleado;
            ruta.Dia= rutaDto.Dia;
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
