using System.ComponentModel.DataAnnotations;

namespace Ventas.Core.Entidades
{
    public class Ruta
    {
        [Key]
        public int IdRuta { get; set; }
        public string CodigoRuta { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoEmpleado { get; set; }
        public string Dia {  get; set; }
        public string Orden { get; set; }//sera un numero, referenciando el orden de la ruta
        public string Estado { get; set; } = "Activo";//Cancelado

    }
}
