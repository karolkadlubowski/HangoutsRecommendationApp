using Microsoft.Extensions.Configuration;
using FileStorage.API.Domain.Configuration;

namespace FileStorage.API.Application.Providers
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public ConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ServiceConfiguration GetConfiguration()
            => _configuration.Get<ServiceConfiguration>();
    }
}