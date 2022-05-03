using Library.Shared.Exceptions;
using Library.Shared.Models;
using Venue.API.Domain.Validation;

namespace Venue.API.Domain.ValueObjects
{
    public record VenueName : ValueObject<string>
    {
        public VenueName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException($"{nameof(name)} cannot be null or empty");

            if (name.Length > ValidationRules.MaxVenueNameLength)
                throw new ValidationException($"{nameof(name)} cannot exceed {ValidationRules.MaxVenueNameLength} characters");

            Value = name;
        }
    }
}