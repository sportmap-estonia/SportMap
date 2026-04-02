using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SportMap.DAL.DataContext;

namespace SportMap.DAL.DataAccess
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<AppDbContextFactory>()
                .Build();

            var host = config["postgres-host"];
            var port = config["postgres-port"];
            var db = config["postgres-database"];
            var user = config["postgres-username"];
            var pass = config["postgres-password"];
            
            var connectionString =
                $"Host={host};Port={port};Database={db};Username={user};Password={pass}";

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            return new AppDbContext(options);
        }
    }
}