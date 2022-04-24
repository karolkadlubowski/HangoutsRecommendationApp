using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserProfile.API.Application.Abstractions;
using UserProfile.API.Application.Services;

namespace UserProfile.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IReadOnlyUserProfileService, UserProfileService>()
                .AddScoped<IUserProfileService, UserProfileService>();
            return services;
        }
    }
}