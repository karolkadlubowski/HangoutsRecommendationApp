using Library.Shared.Exceptions;
using Library.Shared.Models;
using VenueReview.API.Domain.Validation;

namespace VenueReview.API.Domain.ValueObjects
{
    public record ReviewRating : ValueObject<double>
    {
        public ReviewRating(double rating)
        {
            if (rating < ValidationRules.MinRating || rating > ValidationRules.MaxRating)
                throw new ValidationException($"{nameof(rating)} must have value between {ValidationRules.MinRating} and {ValidationRules.MaxRating}");

            Value = rating;
        }
    }
}