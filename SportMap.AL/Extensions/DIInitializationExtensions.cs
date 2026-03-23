using Microsoft.Extensions.DependencyInjection;
using SportMap.AL.Abstractions.Services;
using SportMap.AL.Common;

namespace SportMap.Al.Extensions
{
    public static class DIInitializationExtensions
    {
        public static void AddALServices(this IServiceCollection serviceCollection)
        {
            // TODO Register Services
            serviceCollection.AddScoped<IAuthService, AuthService>();
        }
    }
}
