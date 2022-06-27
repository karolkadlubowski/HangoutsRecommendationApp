using FluentValidation;

namespace VenueList.API.Application.Features.GetUserFavorites
{
    public class GetUserFavoritesValidator : AbstractValidator<GetUserFavoritesQuery>
    {
        public GetUserFavoritesValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}