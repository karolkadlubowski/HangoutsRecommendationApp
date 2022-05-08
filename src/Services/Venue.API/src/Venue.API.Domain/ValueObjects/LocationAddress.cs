using Library.Shared.Exceptions;
using Library.Shared.Models;
using Venue.API.Domain.Validation;

namespace Venue.API.Domain.ValueObjects
{
    public record LocationAddress : ValueObject<string>
    {
        public LocationAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ValidationException($"{nameof(address)} cannot be null or empty");

            if (address.Length > ValidationRules.MaxAddressLength)
                throw new ValidationException($"{nameof(address)} cannot exceed {ValidationRules.MaxAddressLength} characters");

            Value = address;
        }
    }
}