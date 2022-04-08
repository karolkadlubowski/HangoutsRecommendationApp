namespace Library.Shared.Models
{
    public abstract record ValueObject<T>
    {
        public T Value { get; init; }
    }
}