using AccountDefinition.API.Application.Database.Repositories;
using AccountDefinition.API.Domain.Configuration;
using AccountDefinition.API.Infrastructure.Database;
using AccountDefinition.API.Infrastructure.Database.Repositories;
using Library.Database.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountDefinition.API.DI
{
    public static class DatabaseDIConfig
    {
        public static IServiceCollection AddAccountDefinitionDbContext(this IServiceCollection services, IConfiguration configuration)
            => services.InjectDbContext(_ => new AccountDefinitionDbContext(
                configuration.Get<ServiceConfiguration>()
                    .DatabaseConfig.DatabaseConnectionString));

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddTransient<IAccountTypeRepository, AccountTypeRepository>();
    }
}