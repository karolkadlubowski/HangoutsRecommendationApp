using FluentValidation;

namespace VenueList.API.Application.Features.GetFavorites
{
    public class GetFavoritesValidator : AbstractValidator<GetFavoritesQuery>
    {
        public GetFavoritesValidator()
        {
            RuleFor(x => x.UserId).GreaterThanOrEqualTo(0);
            
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}