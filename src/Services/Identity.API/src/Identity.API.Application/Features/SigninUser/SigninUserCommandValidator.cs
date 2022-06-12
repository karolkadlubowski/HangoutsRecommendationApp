using FluentValidation;
using Identity.API.Domain.Validation;

namespace Identity.API.Application.Features.SigninUser
{
    public class SigninUserCommandValidator : AbstractValidator<SigninUserCommand>
    {
        public SigninUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .MaximumLength(ValidationRules.MaxEmailAddress)
                .NotNull();

            RuleFor(x => x.Password)
                .NotNull()
                .Length(ValidationRules.MinPasswordLength, ValidationRules.MaxPasswordLength);
        }
    }
}