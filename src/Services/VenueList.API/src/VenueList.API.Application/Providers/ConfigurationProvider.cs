using Microsoft.Extensions.Configuration;
using VenueList.API.Domain.Configuration;

namespace VenueList.API.Application.Providers
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