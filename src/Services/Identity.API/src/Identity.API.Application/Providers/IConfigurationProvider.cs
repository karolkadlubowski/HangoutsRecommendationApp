using Identity.API.Domain.Configuration;

namespace Identity.API.Application.Providers
{
    public interface IConfigurationProvider
    {
        ServiceConfiguration GetConfiguration();
    }
}