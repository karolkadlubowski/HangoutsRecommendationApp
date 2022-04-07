namespace Library.Shared.HttpAccessor
{
    public interface IReadOnlyHttpAccessor
    {
        bool IsAuthenticated { get; }
        long CurrentUserId { get; }
        string CurrentJwtToken { get; }
    }
}