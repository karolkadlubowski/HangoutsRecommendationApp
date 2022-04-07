using System;
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
    }
}