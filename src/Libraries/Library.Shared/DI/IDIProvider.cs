namespace Library.Shared.DI
{
    public interface IDIProvider
    {
        TService ResolveService<TService>();
    }
}