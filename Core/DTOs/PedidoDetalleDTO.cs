namespace Ventas.Core.DTOs
{
    public class PedidoDetalleDTO
    {
        public string CodigoProducto { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitarioVenta { get; set; }
    }
}
