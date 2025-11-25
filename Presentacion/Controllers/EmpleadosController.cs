using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ventas.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public EmpleadosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet("Empleados")]
        public async Task<IActionResult> GetPedidosCliente()
        {
            string URL = "https://rrhhcloud2-production.up.railway.app/api/Empleados/GET";
            HttpResponseMessage respuesta = await _httpClient.GetAsync(URL);

            if (!respuesta.IsSuccessStatusCode)
            {

                return StatusCode((int)respuesta.StatusCode,
                                  $"Error al obtener los datos del microservicio de Recursos Humanos: {respuesta.ReasonPhrase}");
            }
            string jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
            JsonElement datos = JsonSerializer.Deserialize<JsonElement>(jsonRespuesta);
            return Ok(datos);
        }
    }
}
