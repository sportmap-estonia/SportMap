using Microsoft.Extensions.DependencyInjection;

namespace SportMap.DAL.Extensions
{
    public static class DIInitializationExtensions
    {
        public static void AddDALServices(this IServiceCollection serviceCollection)
        {
            // TODO Register UnitOfWork, Repositories and DbContext
            //serviceCollection.AddDbContext<AppDbContext>(options => options.UseSqlServer());
            //var connectionString =
            //    builder.Configuration.GetConnectionString("DefaultConnection")
            //    ?? throw new InvalidOperationException("Connection string"
            //                                           + "'DefaultConnection' not found.");
        }
    }
}
