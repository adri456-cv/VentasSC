using System.ComponentModel.DataAnnotations;

namespace Ventas.Core.Entidades
{
    public class Ruta
    {
        [Key]
        public int IdRuta { get; set; }
        public string CodigoRuta { get; set; }//se repite para toda la ruta 
        public string CodigoCliente { get; set; }
        //borrar 
        public string CodigoPedido { get; set; }//solo un pedido 
        public DateOnly FechaCreacion { get; set; } = DateOnly.FromDateTime(DateTime.Now);//repite para toda la ruta 
        public string Orden { get; set; }//sera un numero, referenciando el orden.
        public string Estado { get; set; } = "activo";//cancelado

    }
}
