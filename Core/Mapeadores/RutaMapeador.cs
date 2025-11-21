using Ventas.Core.Entidades;
using Ventas.Core.DTOs;
using Humanizer;

namespace Ventas.Core.Mapeadores
{
    public static class RutaMapeador
    {
        public static RutaDTO toRutaDTO(this Ruta dto)
        {
            return new RutaDTO()
            {
                CodigoRuta = dto.CodigoRuta,
                CodigoCliente = dto.CodigoCliente,
                CodigoEmpleado = dto.CodigoEmpleado,
                Dia=dto.Dia,
                Orden=dto.Orden,
            };
        }
        public static Ruta toRuta (this RutaDTO ruta)
        {
            return new Ruta()
            {
                CodigoRuta = ruta.CodigoRuta,
                CodigoCliente = ruta.CodigoCliente,
                CodigoEmpleado = ruta.CodigoEmpleado,
                Dia = ruta.Dia,
                Orden = ruta.Orden,
                Estado = "Activo"
            };
        }
    }
}
