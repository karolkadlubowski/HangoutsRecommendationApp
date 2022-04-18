using Library.Shared.Models.Category.Dtos;
using Library.Shared.Models.Response;

namespace Category.API.Application.Features.AddCategory
{
    public record AddCategoryResponse : BaseApiResponse
    {
        public CategoryDto AddedCategory { get; init; }

        public AddCategoryResponse(Error error = null) : base(error)
        {
        }
    }
}