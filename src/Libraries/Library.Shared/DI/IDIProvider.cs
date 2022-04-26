namespace Library.Shared.DI
{
    public interface IDIProvider
    {
        IDIScope CreateScope();

        TService ResolveService<TService>();
        TService ResolveServiceWhere<TService, TImplementation>() where TImplementation : TService;
    }
}