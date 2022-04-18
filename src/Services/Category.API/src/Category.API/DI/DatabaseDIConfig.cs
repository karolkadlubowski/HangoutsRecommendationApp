using Category.API.Application.Database.Repositories;
using Category.API.Domain.Configuration;
using Category.API.Infrastructure.Database;
using Category.API.Infrastructure.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Category.API.DI
{
    public static class DatabaseDIConfig
    {
        public static IServiceCollection AddCategoryDbContext(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddTransient<CategoryDbContext>()
                .Configure<DatabaseConfig>(configuration.GetSection(nameof(DatabaseConfig)))
                .AddSingleton<DatabaseConfig>(s => s.GetRequiredService<IOptions<DatabaseConfig>>().Value);

        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddTransient<ICategoryRepository, CategoryRepository>();
    }
}