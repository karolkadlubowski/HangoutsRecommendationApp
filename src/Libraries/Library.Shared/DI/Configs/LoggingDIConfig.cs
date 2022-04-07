using Library.Shared.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Shared.DI.Configs
{
    public static class LoggingDIConfig
    {
        public static IServiceCollection AddLoggingProvider(this IServiceCollection services)
            => services.AddSingleton<ILogger, Logger>();
    }
}