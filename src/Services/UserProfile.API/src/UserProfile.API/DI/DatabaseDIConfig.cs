using Microsoft.Extensions.DependencyInjection;
using UserProfile.API.Application.Database.Repositories;
using UserProfile.API.Infrastructure.Database.Repositories;

namespace UserProfile.API.DI
{
    public static class DatabaseDIConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddScoped<IUserProfileRepository, UserProfileRepository>();
    }
}