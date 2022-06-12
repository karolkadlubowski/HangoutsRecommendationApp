using FluentValidation;
using Identity.API.Domain.Validation;

namespace Identity.API.Application.Features.SignupUser
{
    public class SignupUserCommandValidator : AbstractValidator<SignupUserCommand>
    {
        public SignupUserCommandValidator()
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