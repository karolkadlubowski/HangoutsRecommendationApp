using FluentValidation;
using Identity.API.Domain.Validation;

namespace Identity.API.Application.Features.ChangeUserEmail
{
    public class ChangeUserEmailCommandValidator : AbstractValidator<ChangeUserEmailCommand>
    {
        public ChangeUserEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .MaximumLength(ValidationRules.MaxEmailAddressLength)
                .NotNull();
        }
    }
}