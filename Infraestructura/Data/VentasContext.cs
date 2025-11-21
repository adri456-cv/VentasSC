using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ventas.Core.Entidades;

namespace Ventas.Infraestructura.Data
{
    public class VentasContext : DbContext
    {
        public VentasContext (DbContextOptions<VentasContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; } = default!;
        public DbSet<DetallePedido> DetallePedido { get; set; } = default!;
        public DbSet<Pedido> Pedido { get; set; } = default!;
        public DbSet<Venta> Venta { get; set; } = default!;
        public DbSet<Ventas.Core.Entidades.Ruta> Ruta { get; set; } = default!;
    }
}
