using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Loja.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<LojaDbContext>
    {
        public LojaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LojaDbContext>();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)));

            return new LojaDbContext(optionsBuilder.Options);
        }
    }
}
