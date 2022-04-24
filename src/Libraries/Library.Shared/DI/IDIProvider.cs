namespace Library.Shared.DI
{
    public interface IDIProvider
    {
        TService ResolveService<TService>();
        TService ResolveServiceWhere<TService, TImplementation>() where TImplementation : TService;
    }
}