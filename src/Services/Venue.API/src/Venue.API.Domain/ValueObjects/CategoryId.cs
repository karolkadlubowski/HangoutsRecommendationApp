using Library.Shared.Exceptions;
using Library.Shared.Models;
using Venue.API.Domain.Validation;

namespace Venue.API.Domain.ValueObjects
{
    public record CategoryId : ValueObject<string>
    {
        public CategoryId(string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                throw new ValidationException($"{nameof(categoryId)} cannot be null or empty");

            Value = categoryId.Length == ValidationRules.CategoryIdLength
                ? categoryId
                : throw new ValidationException($"{nameof(categoryId)} must have exactly {ValidationRules.CategoryIdLength} characters");
        }
    }
}