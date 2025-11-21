namespace Ventas.Core.DTOs
{
    public class PedidoDTO
    {
        public string Codigo { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoEmpleado { get; set; }
        public string CodigoSucursal { get; set; }
        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;
        public double MontoTotalPedido { get; set; }
    }
}
