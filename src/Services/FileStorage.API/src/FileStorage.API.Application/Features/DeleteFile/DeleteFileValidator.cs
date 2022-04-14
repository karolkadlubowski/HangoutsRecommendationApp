using FluentValidation;

namespace FileStorage.API.Application.Features.DeleteFile
{
    public class DeleteFileValidator : AbstractValidator<DeleteFileCommand>
    {
        public DeleteFileValidator()
        {
            RuleFor(x => x.FileId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.FolderKey)
                .NotNull()
                .NotEmpty();
        }
    }
}