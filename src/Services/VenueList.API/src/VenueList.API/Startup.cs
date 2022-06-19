using System.Reflection;
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
using VenueList.API.Application.Mapper;
using VenueList.API.DI;
using VenueList.API.Infrastructure.HostedServices;
using IConfigurationProvider = VenueList.API.Application.Providers.IConfigurationProvider;

namespace VenueList.API
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
                "VenueList.API.Application");
            
            services.AddVenueListDbContext(Configuration);
            _logger.Trace("> VenueList database context registered");
            
            services.AddMemoryCache(Configuration);
            _logger.Trace("> Memory cache registered");

            services.AddRepositories();
            _logger.Trace("> Database repositories registered");
            
            services.AddServices(Configuration);
            _logger.Trace("> Services registered");

            services.AddSingleton<IConfigurationProvider, Application.Providers.ConfigurationProvider>();
            _logger.Trace("> Configuration provider registered");
            
            services
                .AddHostedService<CategoryDataHostedService>();
            _logger.Trace("> Hosted services registered");
            
            services.AddRetryPolicyRegistry()
                .RegisterAllTypes<IRetryPolicy>(new[] { Assembly.Load("VenueList.API.Infrastructure") }, ServiceLifetime.Singleton);

            _logger.Trace("> Retry policy registry registered");

            services.AddHealthChecks();
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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VenueList.API v1"));

            app.UseHealthChecks("/health");

            app.UseRouting();

            app.UseAuthorization();

            app.UseLoggingRequestScope();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}