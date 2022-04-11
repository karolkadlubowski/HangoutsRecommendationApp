using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace AccountDefinition.API.Domain.ValueObjects
{
    public record ProviderName : ValueObject<string>
    {
        public ProviderName(string provider)
            => Value = string.IsNullOrWhiteSpace(provider)
                ? throw new ValidationException($"{nameof(provider)} cannot be null or empty")
                : provider
                    .Trim()
                    .ToUpperInvariant();
    }
}