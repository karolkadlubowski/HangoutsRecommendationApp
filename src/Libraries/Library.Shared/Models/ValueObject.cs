namespace Library.Shared.Models
{
    public abstract record ValueObject<T>
    {
        public T Value { get; init; }

        public static implicit operator T(ValueObject<T> instance) => instance.Value;
    }
}