namespace Ventas.Core.DTOs
{
    public class DetallePedidoDTO
    {
        public string Codigo { get; set; }
        public string CodigoPedido { get; set; }
        public string CodigoProducto { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitarioVenta { get; set; }//creo que no debe estar
    }
}
