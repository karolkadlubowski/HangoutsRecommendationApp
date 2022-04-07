using Microsoft.Extensions.Configuration;
using Template.API.Domain.Configuration;

namespace Template.API.Application.Providers
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