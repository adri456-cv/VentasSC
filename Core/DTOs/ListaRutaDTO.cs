namespace Ventas.Core.DTOs
{
    public class ListaRutaDTO
    {
        public string CodigoRuta { get; set; }
        public DateOnly FechaCreacion { get; set; }
        public List<DireccionesDTO> Paradas { get; set; }


    }
}
