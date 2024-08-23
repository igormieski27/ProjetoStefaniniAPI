using Microsoft.EntityFrameworkCore;
using PedidosApi.Models;

namespace PedidosApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItensPedido> ItensPedido { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração de ItensPedido, com chave primária Id e relações
            modelBuilder.Entity<ItensPedido>()
                .HasKey(ip => ip.Id);

            modelBuilder.Entity<ItensPedido>()
                .HasOne(ip => ip.Pedido)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(ip => ip.IdPedido)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItensPedido>()
                .HasOne(ip => ip.Produto)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(ip => ip.IdProduto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
