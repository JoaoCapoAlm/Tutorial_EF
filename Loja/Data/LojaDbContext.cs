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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasOne(x => x.Fornecedor)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.FornecedorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
