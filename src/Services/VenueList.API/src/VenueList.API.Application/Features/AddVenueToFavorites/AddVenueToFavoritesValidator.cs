using FluentValidation;
using VenueList.API.Domain.Validation;

namespace VenueList.API.Application.Features.AddVenueToFavorites
{
    public class AddVenueToFavoritesValidator : AbstractValidator<AddVenueToFavoritesCommand>
    {
        public AddVenueToFavoritesValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(ValidationRules.MaxVenueNameLength);

            RuleFor(x => x.Description)
                .MaximumLength(ValidationRules.MaxVenueDescriptionLength);

            RuleFor(x => x.VenueId)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.CreatorId)
                .GreaterThanOrEqualTo(0);
        }
    }
}