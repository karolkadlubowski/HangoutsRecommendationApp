using FluentValidation;
using VenueReview.API.Domain.Validation;

namespace VenueReview.API.Application.Features.AddVenueReview
{
    public class AddVenueReviewValidator : AbstractValidator<AddVenueReviewCommand>
    {
        public AddVenueReviewValidator()
        {
            RuleFor(x => x.Rating)
                .InclusiveBetween(ValidationRules.MinRating, ValidationRules.MaxRating);
        }
    }
}