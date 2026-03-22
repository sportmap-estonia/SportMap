using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportMap.DAL.Common;
using SportMap.DAL.DataAccess;
using SportMap.DAL.DataContext;

namespace SportMap.DAL.Extensions
{
    public static class DIInitializationExtensions
    {
        public static void AddDALServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContextPool<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("sportmapdb")));
            
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
