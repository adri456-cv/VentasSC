namespace Ventas.Core.DTOs
{
    public class VentaDTO
    {
        //no da el get{codigo}, el eliminar y el actualizar estado 
        public string CodigoPedido { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoSucursal { get; set; }
        public string CodigoVenta { get; set; }
        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;
        public double MontoTotal { get; set; }
    }
}
