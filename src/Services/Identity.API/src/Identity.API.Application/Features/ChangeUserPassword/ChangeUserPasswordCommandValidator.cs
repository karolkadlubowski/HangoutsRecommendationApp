using FluentValidation;
using Identity.API.Domain.Validation;

namespace Identity.API.Application.Features.ChangeUserPassword
{
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(x => x.Password)
                .NotNull()
                .Length(ValidationRules.MinPasswordLength, ValidationRules.MaxPasswordLength);
        }
    }
}