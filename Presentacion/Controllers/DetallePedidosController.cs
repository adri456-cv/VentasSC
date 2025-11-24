using System;
using System.Collections.Generic;
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

namespace Ventas.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidosController : ControllerBase
    {
        private readonly IDetallePedidoRepositorio context;

        public DetallePedidosController(IDetallePedidoRepositorio context)
        {
            this.context = context;
        }
        

        
        // GET: api/DetallePedidos
        [HttpGet("ListaDetallesActivos")]
        public async Task<IActionResult> GetDetallePedido()
        {
            List<DetallePedidoDTO> detalle = await context.GetListaDetalle();
            if (detalle == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(detalle);
            }
        }
        
        // GET: api/DetallePedidos/5
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetDetallePedido(string codigo)
        {
            var detallePedido = await context.GetDetalle(codigo);
            if (detallePedido == null)
            {
                return NotFound();
            }
            return Ok(detallePedido);

            
        }

        [HttpGet("ProductoCantidad")]
        public async Task<IActionResult> GetProductoCantidad()
        {
            var productoCantidad = await context.ProductosTotales();
            if (productoCantidad == null)
            {
                return NotFound();
            }
            return Ok(productoCantidad);
        }

        [HttpGet("ProductosMenosVendidos")]
        public async Task<IActionResult> GetProductosMenosVendidos()
        {
            var productosMenosVendidos = await context.ProductoMenos();
            if (productosMenosVendidos == null)
            {
                return NotFound();
            }
            return Ok(productosMenosVendidos);
        }

        // PUT: api/DetallePedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{codigo}")]
        public async Task<IActionResult> Actualizar(string codigo, string nuevoEstado)
        {
            var actualizado = await context.ActualizarEstado(codigo, nuevoEstado);
            if (actualizado == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/DetallePedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AgregarDetalle")]
        public async Task<IActionResult> PostDetallePedido(string codigo, string codigoPedido, string codigoProducto, int cantidad, double precio)
        {
            DetallePedidoDTO detallePedidoDTO = new DetallePedidoDTO() { Codigo=codigo, CodigoPedido=codigoPedido, CodigoProducto=codigoProducto, Cantidad=cantidad, PrecioUnitarioVenta=precio};
            DetallePedido detalle = detallePedidoDTO.toDetallePedido();
            var nuevoDetalle = await context.AgregarDetalle(detalle);
            DetallePedidoDTO detalleCreado = detalle.toDetallePedidoDTO();
            return NoContent();

        }

        // DELETE: api/DetallePedidos/5
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteDetallePedido(string codigo)
        {
            var eliminado = await context.EliminarDetalle(codigo);
            if (eliminado == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        /*
        private bool DetallePedidoExists(int id)
        {
            return _context.DetallePedido.Any(e => e.IdDetallePedido == id);
        }
        */
        
    }
}









