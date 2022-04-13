using FluentValidation;

namespace FileStorage.API.Application.Features.GetFolderByKey
{
    public class GetFolderByKeyValidator : AbstractValidator<GetFolderByKeyQuery>
    {
        public GetFolderByKeyValidator()
        {
            RuleFor(x => x.FolderKey)
                .NotNull()
                .NotEmpty();
        }
    }
}