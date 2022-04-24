using FluentValidation;
using Venue.API.Domain.Validation;

namespace Venue.API.Application.Features.CreateVenue
{
    public class CreateVenueValidator : AbstractValidator<CreateVenueCommand>
    {
        public CreateVenueValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(ValidationRules.MaxVenueNameLength);

            RuleFor(x => x.Description)
                .MaximumLength(ValidationRules.MaxVenueDescriptionLength);

            RuleFor(x => x.CategoryName)
                .NotNull()
                .NotEmpty();
        }
    }
}