using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ventas.Core.DTOs;
using Ventas.Core.Entidades;
using Ventas.Core.Interfaces;
using Ventas.Core.Mapeadores;
using Ventas.Infraestructura.Data;

namespace Ventas.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentaRepositorio context;

        public VentasController(IVentaRepositorio context)
        {
            this.context = context;
        }

        // GET: api/Ventas
        [HttpGet("Ventas")]
        public async Task<IActionResult> GetVenta()
        {
            List<VentaDTO> Registrada = await context.Getventas();
            if (Registrada == null)
            {
                return NotFound();
            }
            return Ok(Registrada);
        }

        // GET: api/Ventas/5
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetVenta(string codigo)
        {
            VentaDTO venta = await context.GetVenta(codigo);


            if (venta == null)
            {
                return NotFound();
            }

            return Ok(venta);
        }

        // PUT: api/Ventas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutVenta(string codigo, string nuevoEstado)
        {
            var ventaActualizado = await context.ActualizarEstado(codigo, nuevoEstado);
            if (ventaActualizado == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Ventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostVenta(string codigoPedido, string codigoCliente, string codigoSucursal, string codigo, double monto)
        {
            VentaDTO ventaDTO = new VentaDTO() { CodigoPedido = codigoPedido, CodigoCliente = codigoCliente, CodigoSucursal = codigoSucursal, CodigoVenta = codigo, MontoTotal = monto };
            Venta venta = ventaDTO.toVenta();
            await context.AgregarVenta(venta);


            VentaDTO ventaCreada = venta.toVentaDTO();

            return CreatedAtAction("GetVenta", new { CodigoVenta = venta.CodigoVenta }, ventaCreada);
        }

        // DELETE: api/Ventas/5
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteVenta(string codigo)
        {
            var eliminado = await context.EliminarVenta(codigo);
            if (eliminado == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        /*
        private bool VentaExists(int id)
        {
            return _context.Venta.Any(e => e.IdVenta == id);
        }
        */
    }
}
