using FileStorage.API.Domain.Validation;
using FluentValidation;

namespace FileStorage.API.Application.Features.GetBranchByName
{
    public class GetBranchByNameValidator : AbstractValidator<GetBranchByNameQuery>
    {
        public GetBranchByNameValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MaximumLength(ValidationRules.MaximumNameLength);
        }
    }
}