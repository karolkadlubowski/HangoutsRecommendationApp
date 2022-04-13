using FileStorage.API.Domain.Validation;
using FluentValidation;

namespace FileStorage.API.Application.Features.PutFile
{
    public class PutFileValidator : AbstractValidator<PutFileCommand>
    {
        public PutFileValidator()
        {
            RuleFor(x => x.Key)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(ValidationRules.MaximumNameLength);

            RuleFor(x => x.File)
                .NotNull();
        }
    }
}