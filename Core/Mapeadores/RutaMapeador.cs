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
                
                CodigoPedido= dto.CodigoPedido,
                FechaCreacion = dto.FechaCreacion,
                Orden=dto.Orden,
            };
        }
        public static Ruta toRuta (this RutaDTO ruta)
        {
            return new Ruta()
            {
                CodigoRuta = ruta.CodigoRuta,
                CodigoCliente = ruta.CodigoCliente,
     
                CodigoPedido=ruta.CodigoPedido,
                
                Orden = ruta.Orden,
                FechaCreacion = DateOnly.FromDateTime(DateTime.UtcNow), // Usar UTC para consistencia
                Estado = "Activo"
                
            };
        }
    }
}
