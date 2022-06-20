using FluentValidation;

namespace VenueList.API.Application.Features.DeleteFavorite
{
    public class DeleteFavoriteValidator : AbstractValidator<DeleteFavoriteCommand>
    {
        public DeleteFavoriteValidator()
        {
            RuleFor(x => x.VenueId)
                .GreaterThan(0);
        }
    }
}