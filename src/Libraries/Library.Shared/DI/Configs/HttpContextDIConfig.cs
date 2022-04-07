using Library.Shared.HttpAccessor;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Shared.DI.Configs
{
    public static class HttpContextDIConfig
    {
        public static IServiceCollection AddHttpAccessor(this IServiceCollection services)
            => services
                .AddHttpContextAccessor()
                .AddSingleton<IReadOnlyHttpAccessor, HttpAccessor.HttpAccessor>();
    }
}