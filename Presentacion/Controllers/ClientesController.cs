using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepositorio context;

        public ClientesController(IClienteRepositorio context)
        {
            this.context = context;
        }
        
        // GET: api/Clientes
        [HttpGet("ListaDeClientesDeAlta")]
        public async Task<IActionResult> GetListaCliente()
        {
            List<ClienteDTO>cliente=await context.GetListaCliente();
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);

        }
        
        // GET: api/Clientes/5
        [HttpGet("{ci}")]
        public async Task<IActionResult> GetCliente(string ci)
        {
            ClienteDTO cliente = await context.GetCliente(ci);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
        
        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{ci}")]
        public async Task<IActionResult> PutCliente(string ci, string codigo, string nombre, string apellido, string telefono, string contacto, string direccion)
        {
            ClienteDTO clienteDTO = new ClienteDTO() { Codigo = codigo, Nombre = nombre, Apellido = apellido, Telefono = telefono, Contacto = contacto, Direccion = direccion };
            ClienteDTO clienteActualizado = await context.ActualizarCliente(ci, clienteDTO);
            if (clienteActualizado == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        
        // POST: api/Clientes
        // 1. Pemite crear un nuevo cliente en el sistema, naturalmente
        // al crearlo este tendra un estado de "alta" y podra realizar pedidos. 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Tarea3CrearCliente")]
        public async Task<IActionResult> PostCliente(string ci, string nombre, string apellido, string telefono, string contacto, string direccion)
        {
            ClienteDTO clienteDTO = new ClienteDTO() { Codigo=ci, Nombre=nombre, Apellido=apellido, Telefono=telefono, Contacto=contacto, Direccion=direccion};
            Cliente cliente = clienteDTO.toCliente();
            await context.AgregarCliente(cliente);
            

            ClienteDTO clienteCreado = cliente.toClienteDTO();

            return CreatedAtAction("GetCliente", new { ci = cliente.Codigo }, clienteCreado);

        }
        
        // DELETE: api/Clientes/5
        [HttpDelete("{ci}")]
        public async Task<IActionResult> DeleteCliente(string ci)
        {
            var eliminado = await context.EliminarCliente(ci);
            if (eliminado == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        /*
        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.IdCliente == id);
        }
        */
        
        
    }
}
