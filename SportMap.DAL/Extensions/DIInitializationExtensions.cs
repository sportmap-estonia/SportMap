using Microsoft.Extensions.DependencyInjection;
using SportMap.DAL.Abstractions;
using SportMap.DAL.DataAccess;

namespace SportMap.DAL.Extensions
{
    public static class DIInitializationExtensions
    {
        public static void AddDALServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IUnitOfWork, UnitOfWork>();
            // TODO Register UnitOfWork, Repositories and DbContext
            //serviceCollection.AddDbContext<AppDbContext>(options => options.UseSqlServer());
            //var connectionString =
            //    builder.Configuration.GetConnectionString("DefaultConnection")
            //    ?? throw new InvalidOperationException("Connection string"
            //                                           + "'DefaultConnection' not found.");
        }
    }
}
