using FluentValidation;
using Identity.API.Domain.Validation;

namespace Identity.API.Application.Features.SignupUser
{
    public class SignupUserValidator : AbstractValidator<SignupUserCommand>
    {
        public SignupUserValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(ValidationRules.MaxEmailAddressLength);

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .Length(ValidationRules.MinPasswordLength, ValidationRules.MaxPasswordLength);
        }
    }
}