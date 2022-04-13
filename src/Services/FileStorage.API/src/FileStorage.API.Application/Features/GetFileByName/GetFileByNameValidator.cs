using FluentValidation;

namespace FileStorage.API.Application.Features.GetFileByName
{
    public class GetFileByNameValidator : AbstractValidator<GetFileByNameQuery>
    {
        public GetFileByNameValidator()
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