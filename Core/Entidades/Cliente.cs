using System.ComponentModel.DataAnnotations;

namespace Ventas.Core.Entidades
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } = "Alta";//alta o baja

    }
}
