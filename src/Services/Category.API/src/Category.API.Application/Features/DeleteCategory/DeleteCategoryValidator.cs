using FluentValidation;

namespace Category.API.Application.Features.DeleteCategory
{
    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotNull()
                .NotEmpty();
        }
    }
}