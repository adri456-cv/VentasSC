using Ventas.Core.DTOs;
using Ventas.Core.Entidades;

namespace Ventas.Core.Mapeadores
{
    public static class ClienteMapeador
    {
        public static ClienteDTO toClienteDTO(this Cliente cliente)
        {
            return new ClienteDTO()
            {
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Codigo = cliente.Codigo,
                Contacto = cliente.Contacto,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion
            };
        }
        public static Cliente toCliente(this ClienteDTO dto)
        {
            return new Cliente()
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Contacto = dto.Contacto,
                Direccion = dto.Direccion,
                Estado = "alta" // Estado por defecto
            };
        }

    }
}
