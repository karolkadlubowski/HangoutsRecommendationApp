using MediatR;

namespace Category.API.Application.Features.DeleteCategory
{
    public record DeleteCategoryCommand : IRequest<DeleteCategoryResponse>
    {
        public string CategoryId { get; init; }
    }
}