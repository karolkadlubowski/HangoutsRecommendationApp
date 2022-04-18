using Category.API.Domain.Configuration;

namespace Category.API.Application.Providers
{
    public interface IConfigurationProvider
    {
        ServiceConfiguration GetConfiguration();
    }
}