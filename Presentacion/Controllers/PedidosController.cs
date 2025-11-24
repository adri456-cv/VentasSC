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
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepositorio context;

        public PedidosController(IPedidoRepositorio context)
        {
            this.context = context;
        }
        [HttpGet("ListaDePedidos")]
        public async Task<IActionResult> GetListaPedido()
        {
            List<PedidoDTO> pedido = await context.GetPedidosTodos();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
        // 2. GET: api/Pedidos
        //Permite visualizar todos los pedidos registrados en el sistema.
        [HttpGet("ListarPedidosEntregados")]
        public async Task<IActionResult> GetPedido()
        {
            List<PedidoDTO> pedido = await context.GetPedido();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);

        }
        [HttpGet("ListarPedidosEnProceso")]
        public async Task<IActionResult> GetPedidosEnProceso()
        {
            List<PedidoDTO> pedido = await context.GetEnProceso();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
        [HttpGet("ListarPedidosCancelados")]
        public async Task<IActionResult> GetPedidosCancelados()
        {
            List<PedidoDTO> pedido = await context.GetCancelados();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        // 3. GET: api/Pedidos/5
        //Permite obtener los atributos de un pedido específico utilizando su codigo.
        [HttpGet("ObtenerPedido{codigo}")]
        public async Task<IActionResult> GetPedido(string codigo)
        {
            PedidoDTO pedido = await context.GetPedido(codigo);

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpGet("MostrarDetalle/{codigo}")]
        public async Task<IActionResult> GetMostrarDetalle([FromRoute]string codigo)
        {
            List<PedidoDetalleDTO> detallePedidos = await context.GetPedidosConDetalles(codigo);
            if (detallePedidos == null)
            {
                return NotFound();
            }
            return Ok(detallePedidos);
        }

        // 4. PUT: api/Pedidos/5
        // Permite actualizar el estado de un pedido existente.
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Tarea3ActualizarEstado/{codigo}")]
        public async Task<IActionResult> PutPedido(string codigo, string estadoNuevo)
        {
            var pedidoActualizado = await context.ActualizarEstado(codigo, estadoNuevo);
            if (pedidoActualizado== null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // 5. POST: api/Pedidos
        // Permite crear un nuevo pedido para los clientes que
        // esten dados de alta en el sistema.
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Tarea3AgregarPedido")]
        public async Task<IActionResult> PostPedido(string codigo, string codigoCliente, string codigoEmpleado )
        {
            
            PedidoDTO pedidoDTO = new PedidoDTO() { Codigo=codigo, CodigoCliente=codigoCliente,CodigoEmpleado=codigoEmpleado};
            Pedido pedido = pedidoDTO.toPedido();
            var nuevoPedido = await context.AgregarPedido(pedido);

            PedidoDTO pedidoCreado = pedido.toPedidoDTO();

            return CreatedAtAction("GetPedido", new { id = pedido.IdPedido }, pedidoCreado);
        }
        
        // DELETE: api/Pedidos/5
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeletePedido(string codigo)
        {
            var eliminado = await context.EliminarPedido(codigo);
            if (eliminado == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        /*
        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.IdPedido == id);
        }
        */
    }
}
