using System;

namespace Library.Shared.DI
{
    public interface IDIScope : IDisposable
    {
        TService ResolveService<TService>();
    }
}