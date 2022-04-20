using Library.Shared.Exceptions;
using Library.Shared.Models;
using Venue.API.Domain.Validation;

namespace Venue.API.Domain.ValueObjects
{
    public record VenueDescription : ValueObject<string>
    {
        public VenueDescription(string description)
        {
            if (description is not null)
                Value = description.Length <= ValidationRules.MaxVenueDescriptionLength
                    ? description
                    : throw new ValidationException($"{nameof(description)} cannot exceed {ValidationRules.MaxVenueDescriptionLength} characters");
        }
    }
}