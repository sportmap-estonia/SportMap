using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportMap.DAL.Abstractions;
using SportMap.DAL.Abstractions.Repositories;
using SportMap.DAL.DataAccess;
using SportMap.DAL.DataContext;
using SportMap.DAL.Repositories;

namespace SportMap.DAL.Extensions
{
    public static class DIInitializationExtensions
    {
        public static void AddDALServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContextPool<AppDbContext>(options => 
                options.UseNpgsql(configuration.GetConnectionString("sportmapdb")));

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IPostRepository, PostRepository>();
        }
    }
}
