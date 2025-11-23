namespace Ventas.Core.DTOs
{
    public class ListaRutaDTO
    {
        public string CodigoRuta { get; set; }
        public string CodigoPedido { get; set; }
        public string Direccion { get; set; }
        public DateOnly FechaCreacion { get; set; }
        public string Orden { get; set; }
    }
}
