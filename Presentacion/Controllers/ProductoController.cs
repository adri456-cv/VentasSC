using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ventas.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
       
        private readonly HttpClient _httpClient;
        public ProductoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet("Productos")]
        public async Task<IActionResult> GetPedidosCliente()
        {
            string URL = "https://almacensanc-production.up.railway.app/api/Producto";
            HttpResponseMessage respuesta = await _httpClient.GetAsync(URL);

            if (!respuesta.IsSuccessStatusCode)
            {
                
                return StatusCode((int)respuesta.StatusCode,
                                  $"Error al obtener los datos del microservicio Producto: {respuesta.ReasonPhrase}");
            }
            string jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
            JsonElement datos = JsonSerializer.Deserialize<JsonElement>(jsonRespuesta);
            return Ok(datos);
        }
    }
}