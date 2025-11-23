using System.ComponentModel.DataAnnotations;

namespace Ventas.Core.Entidades
{
    public class DetallePedido
    {
        [Key]
        public int IdDetallePedido { get; set; }
        public string Codigo { get; set; }
        public string CodigoPedido { get; set; }
        public string CodigoProducto { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitarioVenta { get; set; }
        public double SubTotal { get; set; }
        public string Estado { get; set; } = "activo";//Cancelado
    }
}
