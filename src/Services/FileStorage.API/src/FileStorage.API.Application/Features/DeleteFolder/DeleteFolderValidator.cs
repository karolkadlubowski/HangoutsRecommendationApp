using FluentValidation;

namespace FileStorage.API.Application.Features.DeleteFolder
{
    public class DeleteFolderValidator : AbstractValidator<DeleteFolderCommand>
    {
        public DeleteFolderValidator()
        {
            RuleFor(x => x.FolderKey)
                .NotNull()
                .NotEmpty();
        }
    }
}