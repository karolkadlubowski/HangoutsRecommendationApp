using Category.API.Domain.Validation;
using FluentValidation;

namespace Category.API.Application.Features.AddCategory
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(ValidationRules.MaxNameLength);
        }
    }
}