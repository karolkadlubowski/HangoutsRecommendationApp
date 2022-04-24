using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Shared.DI
{
    public class DIProvider : IDIProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public DIProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TService ResolveService<TService>()
            => _serviceProvider.GetRequiredService<TService>();

        public TService ResolveServiceWhere<TService, TImplementation>()
            where TImplementation : TService
            => _serviceProvider.GetServices<TService>()
                .SingleOrDefault(s => s.GetType() == typeof(TImplementation));
    }
}