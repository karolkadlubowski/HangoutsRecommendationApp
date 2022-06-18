using System.Reflection;
using Library.Database.DI;
using Library.Shared.DI;
using Library.Shared.DI.Configs;
using Library.Shared.Extensions;
using Library.Shared.Policies.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Venue.API.Application.Mapper;
using Venue.API.DI;
using Venue.API.HealthChecks;
using Venue.API.Infrastructure.HostedServices;
using IConfigurationProvider = Venue.API.Application.Providers.IConfigurationProvider;

namespace Venue.API
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            _logger.Info("Register application configuration started...");

            services.InjectDefaultConfigs(_logger,
                Configuration,
                "Venue.API.Application");

            services
                .AddVenueDbContext(Configuration)
                .AddTransactionManager();
            _logger.Trace("> Venue database context registered");

            services.AddRepositories();
            _logger.Trace("> Database repositories registered");

            services.AddMemoryCache(Configuration);
            _logger.Trace("> Memory cache registered");

            services
                .AddKafkaMessageBroker(Configuration)
                .AddEventHandlersStrategies(Assembly.Load("Venue.API.Application"));
            _logger.Trace("> Kafka message broker registered");

            services.AddServices(Configuration);
            _logger.Trace("> Services registered");

            services.AddSingleton<IConfigurationProvider, Application.Providers.ConfigurationProvider>();
            _logger.Trace("> Configuration provider registered");

            services
                .AddHostedService<EventConsumerHostedService>()
                .AddHostedService<CategoryDataHostedService>();
            _logger.Trace("> Hosted services registered");

            services.AddRetryPolicyRegistry()
                .RegisterAllTypes<IRetryPolicy>(new[] { Assembly.Load("Venue.API.Infrastructure") }, ServiceLifetime.Singleton);
            _logger.Trace("> Retry policy registry registered");

            services
                .AddHealthChecks()
                .AddCheck<DatabaseHealthCheck>(nameof(DatabaseHealthCheck));
            _logger.Trace("> Health checks registered");

            services.AddAutoMapper(typeof(MapperProfile));
            _logger.Trace("> AutoMapper profile registered");

            services.AddSwagger();
            _logger.Trace("> Swagger UI registered");

            _logger.Info("Application registered successfully");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Venue.API v1"));

            app.UseHealthChecks("/health");

            app.UseRouting();

            app.UseAuthorization();

            app.UseLoggingRequestScope();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}