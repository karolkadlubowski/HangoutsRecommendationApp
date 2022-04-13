using FileStorage.API.Domain.Validation;
using FluentValidation;

namespace FileStorage.API.Application.Features.GetFolderByKey
{
    public class GetFolderByKeyValidator : AbstractValidator<GetFolderByKeyQuery>
    {
        public GetFolderByKeyValidator()
        {
            RuleFor(x => x.Key)
                .NotNull()
                .MaximumLength(ValidationRules.MaximumNameLength);
        }
    }
}