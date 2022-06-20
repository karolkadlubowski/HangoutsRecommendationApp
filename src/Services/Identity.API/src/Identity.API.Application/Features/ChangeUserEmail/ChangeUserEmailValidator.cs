using FluentValidation;
using Identity.API.Domain.Validation;

namespace Identity.API.Application.Features.ChangeUserEmail
{
    public class ChangeUserEmailValidator : AbstractValidator<ChangeUserEmailCommand>
    {
        public ChangeUserEmailValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(ValidationRules.MaxEmailAddressLength);
        }
    }
}