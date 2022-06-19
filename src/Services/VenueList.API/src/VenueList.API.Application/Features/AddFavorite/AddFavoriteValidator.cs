using FluentValidation;
using VenueList.API.Domain.Validation;

namespace VenueList.API.Application.Features.AddFavorite
{
    public class AddFavoriteValidator : AbstractValidator<AddFavoriteCommand>
    {
        public AddFavoriteValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(ValidationRules.MaxVenueNameLength);
            
            RuleFor(x => x.Description)
                .MaximumLength(ValidationRules.MaxVenueDescriptionLength);

            RuleFor(x => x.UserId)
                .GreaterThanOrEqualTo(0);
            
            RuleFor(x => x.VenueId)
                .GreaterThanOrEqualTo(0);
            
            RuleFor(x => x.CreatorId)
                .GreaterThanOrEqualTo(0);
        }
    }
}