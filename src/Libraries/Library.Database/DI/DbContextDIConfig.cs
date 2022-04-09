using Library.Database.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Database.DI
{
    public static class DbContextDIConfig
    {
        public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services) where TContext : class, IDbContext
            => services.AddTransient<IDbContext, TContext>();
    }
}