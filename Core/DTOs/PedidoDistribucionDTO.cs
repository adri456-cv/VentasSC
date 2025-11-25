namespace Ventas.Core.DTOs
{
    public class PedidoDistribucionDTO
    {
        public string CodigoPedido { get; set; }
        //public string CodigoCliente { get; set; }
        //public double MontoTotal { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaPedido { get; set; }
        public string EstadoPedido { get; set; }
    }
}
