using Category.API.Domain.Validation;
using Library.Shared.Exceptions;
using Library.Shared.Models;

namespace Category.API.Domain.ValueObjects
{
    public record CategoryName : ValueObject<string>
    {
        public CategoryName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException($"{nameof(name)} cannot be null or empty");

            if (name.Length > ValidationRules.MaxNameLength)
                throw new ValidationException($"{nameof(name)} cannot exceed {ValidationRules.MaxNameLength} characters");

            Value = name
                .Trim()
                .ToUpperInvariant();
        }
    }
}