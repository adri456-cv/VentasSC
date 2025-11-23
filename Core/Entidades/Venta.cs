using System.ComponentModel.DataAnnotations;

namespace Ventas.Core.Entidades
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }
        public string CodigoPedido { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoSucursal { get; set; }
        public string CodigoVenta { get; set; }
        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;
        public double MontoTotal { get; set; }
        public string EstadoVenta { get; set; } = "registrada";//registrada o anulada

    }
}
