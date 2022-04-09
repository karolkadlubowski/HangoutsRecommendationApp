using AccountDefinition.API.Domain.Configuration;

namespace AccountDefinition.API.Application.Providers
{
    public interface IConfigurationProvider
    {
        ServiceConfiguration GetConfiguration();
    }
}