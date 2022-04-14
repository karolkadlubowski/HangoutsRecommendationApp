using FluentValidation;

namespace FileStorage.API.Application.Features.DeleteFile
{
    public class DeleteFileValidator : AbstractValidator<DeleteFileCommand>
    {
        public DeleteFileValidator()
        {
            RuleFor(x => x.FileName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.FolderKey)
                .NotNull()
                .NotEmpty();
        }
    }
}