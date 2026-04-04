using Microsoft.Extensions.DependencyInjection;
using SportMap.AL.Abstractions.Services;
using SportMap.AL.UseCases.Feeds;
using SportMap.AL.UseCases.Images;
using SportMap.DAL.Cache;
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
            serviceCollection.AddScoped<IJwtService, JwtService>();
            serviceCollection.AddSingleton<ICacheService, RedisCacheService>();

            // Posts
            serviceCollection.AddTransient<GetPostQueryHandler>();
            serviceCollection.AddTransient<CreatePostCommandHandler>();

            // Images
            serviceCollection.AddTransient<GetImageQueryHandler>();
            serviceCollection.AddTransient<UploadProfilePictureCommandHandler>();
            serviceCollection.AddTransient<RemoveProfilePictureCommandHandler>();
            serviceCollection.AddTransient<GetOwnProfilePictureQueryHandler>();
        }
    }
}
