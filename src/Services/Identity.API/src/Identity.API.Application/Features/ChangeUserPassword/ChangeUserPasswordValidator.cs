using FluentValidation;
using Identity.API.Domain.Validation;

namespace Identity.API.Application.Features.ChangeUserPassword
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordValidator()
        {
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .Length(ValidationRules.MinPasswordLength, ValidationRules.MaxPasswordLength);
        }
    }
}