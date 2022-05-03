using Venue.API.Domain.Configuration;

namespace Venue.API.Application.Providers
{
    public interface IConfigurationProvider
    {
        ServiceConfiguration GetConfiguration();
    }
}