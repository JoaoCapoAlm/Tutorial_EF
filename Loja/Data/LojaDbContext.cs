using Loja.Models;
using Microsoft.EntityFrameworkCore;

namespace Loja.Data
{
    public class LojaDbContext : DbContext
    {
        public LojaDbContext(DbContextOptions<LojaDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Venda> Venda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasOne(x => x.Fornecedor)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.FornecedorId);

            modelBuilder.Entity<Venda>()
                .HasOne(x => x.Produto)
                .WithMany(x => x.Vendas)
                .HasForeignKey(x => x.ProdutoId);

            modelBuilder.Entity<Venda>()
                .HasOne(x => x.Cliente)
                .WithMany(x => x.Vendas)
                .HasForeignKey(x => x.ClienteId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
