using MediatR;

namespace Category.API.Application.Features.GetCategories
{
    public record GetCategoriesQuery : IRequest<GetCategoriesResponse>;
}