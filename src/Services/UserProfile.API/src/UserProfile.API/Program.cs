using System;
using System.Reflection;
using System.Threading.Tasks;
using Library.Shared.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace UserProfile.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var logger = NLogBuilder.ConfigureNLog(LoggingConstants.NlogFileName).GetCurrentClassLogger();
            var serviceName = Assembly.GetExecutingAssembly().GetName().Name;

            using (host.Services.CreateScope())
            {
                try
                {
                    logger.Info($"Starting {serviceName}...");

                    logger.Info($"Application {serviceName} started");

                    await host.RunAsync();
                }
                catch (Exception e)
                {
                    logger.Fatal(e, "Application terminated unexpectedly...");
                    throw;
                }
                finally
                {
                    LogManager.Shutdown();
                }
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.ClearProviders())
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>());
    }
}