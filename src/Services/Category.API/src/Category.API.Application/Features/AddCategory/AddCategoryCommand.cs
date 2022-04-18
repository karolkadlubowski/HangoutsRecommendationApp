using MediatR;

namespace Category.API.Application.Features.AddCategory
{
    public record AddCategoryCommand : IRequest<AddCategoryResponse>
    {
        public string Name { get; init; }
    }
}