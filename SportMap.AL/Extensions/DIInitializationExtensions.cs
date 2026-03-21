using Microsoft.Extensions.DependencyInjection;
using SportMap.AL.Abstractions.Services;
using SportMap.DAL.Cache;

namespace SportMap.Al.Extensions
{
    public static class DIInitializationExtensions
    {
        public static void AddALServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICacheService, RedisCacheService>();
        }
    }
}
