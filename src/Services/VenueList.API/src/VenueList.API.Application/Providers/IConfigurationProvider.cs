using VenueList.API.Domain.Configuration;

namespace VenueList.API.Application.Providers
{
    public interface IConfigurationProvider
    {
        ServiceConfiguration GetConfiguration();
    }
}