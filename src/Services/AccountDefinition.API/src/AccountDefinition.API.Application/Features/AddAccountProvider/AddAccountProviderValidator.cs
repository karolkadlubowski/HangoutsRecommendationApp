using FluentValidation;

namespace AccountDefinition.API.Application.Features.AddAccountProvider
{
    public class AddAccountProviderValidator : AbstractValidator<AddAccountProviderCommand>
    {
        public AddAccountProviderValidator()
        {
            RuleFor(x => x.Provider)
                .NotNull()
                .NotEmpty();
        }
    }
}