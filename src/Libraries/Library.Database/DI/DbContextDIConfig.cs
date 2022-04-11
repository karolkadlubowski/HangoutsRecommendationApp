using System;
using Library.Database.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Database.DI
{
    public static class DbContextDIConfig
    {
        public static IServiceCollection InjectDbContext<TContext>(this IServiceCollection services,
            Func<IServiceProvider, TContext> implementationFactory)
            where TContext : DbContext, IDbContext
            => services.AddTransient<IDbContext, TContext>(implementationFactory);
    }
}