using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ventas.Core.DTOs;
using Ventas.Core.Entidades;
using Ventas.Core.Interfaces;
using Ventas.Core.Mapeadores;
using Ventas.Infraestructura.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ventas.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutasController : ControllerBase
    {
        private readonly IRutaRepositorio context;

        public RutasController(IRutaRepositorio context)
        {
            this.context = context;
        }

        // GET: api/Rutas
        [HttpGet]
        public async Task<IActionResult> GetRuta()
        {
            List<RutaDTO> ruta = await context.GetRutas();
            if (ruta == null)
            {
                return NotFound();
            }
            return Ok(ruta);
        }

        // GET: api/Rutas/5
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetRuta(string codigo)
        {
            RutaDTO ruta = await context.GetRuta(codigo);

            if (ruta == null)
            {
                return NotFound();
            }

            return Ok(ruta);
        }

        // PUT: api/Rutas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutRuta(string codigo, string codigoRuta, string codigoCliente, string codigoEmpleado, string orden, string dia)
        {
            RutaDTO rutaDTO = new RutaDTO() { CodigoRuta=codigoRuta, CodigoCliente=codigoCliente, CodigoEmpleado=codigoEmpleado, Dia=dia, Orden=orden};
            RutaDTO rutActualizado = await context.ActualizarRuta(codigo,rutaDTO);
            if (rutActualizado == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Rutas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostRuta(string codigoRuta, string codigoCliente, string codigoEmpleado, string orden, string dia)
        {
            RutaDTO rutaDTO = new RutaDTO() { CodigoRuta=codigoRuta, CodigoCliente=codigoCliente, CodigoEmpleado=codigoEmpleado, Orden=orden, Dia=dia};
            Ruta Ruta = rutaDTO.toRuta();
            await context.AgregarRuta(Ruta);


            RutaDTO rutCreado = Ruta.toRutaDTO();

            return CreatedAtAction("GetRuta", new { CodigoRuta = Ruta.CodigoRuta }, rutCreado);
        }

        // DELETE: api/Rutas/5
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteRuta(string codigo)
        {
            var eliminado = await context.EliminarRuta(codigo);
            if (eliminado == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        /*
        private bool RutaExists(int id)
        {
            return _context.Ruta.Any(e => e.IdRuta == id);
        }
        */
    }
}
