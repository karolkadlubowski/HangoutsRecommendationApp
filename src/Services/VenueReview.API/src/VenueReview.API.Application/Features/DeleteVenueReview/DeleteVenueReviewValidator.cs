using FluentValidation;

namespace VenueReview.API.Application.Features.DeleteVenueReview
{
    public class DeleteVenueReviewValidator : AbstractValidator<DeleteVenueReviewCommand>
    {
        public DeleteVenueReviewValidator()
        {
            RuleFor(x => x.VenueReviewId)
                .NotNull()
                .NotEmpty();
        }
    }
}