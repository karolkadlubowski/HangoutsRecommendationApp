using Identity.API.Application.Abstractions;
using Identity.API.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.DI
{
    public static class ServicesDIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddSingleton<IPasswordHashService, Argo2PasswordHashService>()
                .AddSingleton<IAuthTokenService, JwtAuthTokenService>();
    }
}