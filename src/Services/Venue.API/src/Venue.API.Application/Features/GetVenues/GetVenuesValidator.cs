using FluentValidation;

namespace Venue.API.Application.Features.GetVenues
{
    public class GetVenuesValidator : AbstractValidator<GetVenuesQuery>
    {
        public GetVenuesValidator()
        {
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}