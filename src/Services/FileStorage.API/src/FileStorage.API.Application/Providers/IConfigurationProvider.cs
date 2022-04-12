using FileStorage.API.Domain.Configuration;

namespace FileStorage.API.Application.Providers
{
    public interface IConfigurationProvider
    {
        ServiceConfiguration GetConfiguration();
    }
}