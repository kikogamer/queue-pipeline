using App.Core.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Data
{
    public class MeuDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Status)
                .HasConversion(
                    p => p.GetType().Name,
                    p => GetPedidoState(p)
                );

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        private static PedidoState GetPedidoState(string state)
        {
            return state switch
            {
                nameof(PedidoEmAndamento) => new PedidoEmAndamento(),
                nameof(PedidoConfirmado) => new PedidoConfirmado(),
                _ => throw new InvalidOperationException($"Status do Pedido inválido: {state}")
            };
        }
    }
}
