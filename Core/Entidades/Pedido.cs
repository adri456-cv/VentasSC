using System.ComponentModel.DataAnnotations;

namespace Ventas.Core.Entidades
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }
        public string Codigo { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoEmpleado { get; set; }
        
        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;
        public string EstadoPedido { get; set; } = "en proceso"; //en proceso, entregado y cancelado
    }
}
