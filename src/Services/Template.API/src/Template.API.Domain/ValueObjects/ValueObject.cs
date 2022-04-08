namespace Template.API.Domain.ValueObjects
{
    public abstract record ValueObject<T>
    {
        public T Value { get; init; }
    }
}