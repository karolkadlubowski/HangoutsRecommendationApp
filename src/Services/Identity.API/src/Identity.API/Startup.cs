using System.Reflection;
using Identity.API.Application.Mapper;
using Library.Shared.DI;
using Library.Shared.DI.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Identity.API.DI;
using Identity.API.HealthChecks;
using IConfigurationProvider = Identity.API.Application.Providers.IConfigurationProvider;

namespace Identity.API
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
                "Identity.API.Application");

            services.AddIdentityDbContext(Configuration);
            _logger.Trace("> Identity database context registered");

            services.AddRepositories();
            _logger.Trace("> Database repositories registered");

            services.AddKafkaMessageBroker(Configuration);
            _logger.Trace("> Kafka message broker registered");

            services.AddServices(Configuration);
            _logger.Trace("> Services registered");

            services.AddSingleton<IConfigurationProvider, Application.Providers.ConfigurationProvider>();
            _logger.Trace("> Configuration provider registered");

            services
                .AddHealthChecks()
                .AddCheck<DatabaseHealthCheck>(nameof(DatabaseHealthCheck));
            _logger.Trace("> Health checks registered");

            services.AddSwagger();
            _logger.Trace("> Swagger UI registered");

            services.AddAutoMapper(typeof(MapperProfile));
            _logger.Trace("> AutoMapper profile registered");

            _logger.Info("Application registered successfully");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity.API v1"));

            app.UseHealthChecks("/health");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseLoggingRequestScope();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}