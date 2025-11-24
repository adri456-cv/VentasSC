namespace Ventas.Core.DTOs
{
    public class PedidoRutaDTO
    {
        public string CodigoPedido { get; set; }
        public string CodigoCliente { get; set; }
        public double MontoTotal { get; set; }

        public string EstadoPedido { get; set; }
    }
}
