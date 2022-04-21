using Microsoft.Extensions.Configuration;
using Category.API.Domain.Configuration;

namespace Category.API.Application.Providers
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