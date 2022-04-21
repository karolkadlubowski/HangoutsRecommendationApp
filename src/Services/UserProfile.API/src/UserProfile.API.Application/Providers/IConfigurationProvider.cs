using UserProfile.API.Domain.Configuration;

namespace UserProfile.API.Application.Providers
{
    public interface IConfigurationProvider
    {
        ServiceConfiguration GetConfiguration();
    }
}