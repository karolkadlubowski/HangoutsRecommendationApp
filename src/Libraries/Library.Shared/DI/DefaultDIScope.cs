using Microsoft.Extensions.DependencyInjection;

namespace Library.Shared.DI
{
    public class DefaultDIScope : IDIScope
    {
        private readonly IServiceScope _scope;

        public DefaultDIScope(IServiceScope scope) => _scope = scope;

        public TService ResolveService<TService>()
            => _scope.ServiceProvider.GetRequiredService<TService>();

        public void Dispose() => _scope.Dispose();
    }
}