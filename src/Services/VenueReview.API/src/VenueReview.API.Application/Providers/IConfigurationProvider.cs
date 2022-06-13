using VenueReview.API.Domain.Configuration;

namespace VenueReview.API.Application.Providers
{
    public interface IConfigurationProvider
    {
        ServiceConfiguration GetConfiguration();
    }
}