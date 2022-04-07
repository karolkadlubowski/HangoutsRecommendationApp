using Microsoft.Extensions.DependencyInjection;

namespace Library.Shared.DI.Configs
{
    public static class DIProviderDIConfig
    {
        public static IServiceCollection AddDIProvider(this IServiceCollection services)
            => services.AddScoped<IDIProvider, DIProvider>();
    }
}