using System.Reflection;
using System.Text.Json;
using Library.Shared.DI.Configs;
using Library.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace Library.Shared.DI
{
    public static class DefaultAPIDIConfig
    {
        public static IServiceCollection InjectDefaultConfigs(this IServiceCollection services, ILogger logger,
            IConfiguration configuration,
            string mediatrAssemblyName)
        {
            services.AddControllers()
                .AddJsonOptions(_ => new JsonSerializerOptions(JsonOptions.JsonSerializerOptions));

            services.AddValidationFilter();
            logger.Trace("> Custom validation filter registered");

            services.AddMediatRWithValidators(Assembly.Load(mediatrAssemblyName));
            logger.Trace("> MediatR with validators registered");

            services.AddPipelineBehaviours();
            logger.Trace("> Pipelines registered");

            services.AddLoggingProvider();
            logger.Trace("> Logging provider registered");

            services.AddAuthentication(configuration);
            logger.Trace("> Authentication with JWT Bearer registered");

            services.AddDIProvider();
            logger.Trace("> DI provider registered");

            services.AddHttpAccessor();
            logger.Trace("> Http context accessor registered");

            return services;
        }
    }
}