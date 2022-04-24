using Library.Shared.Models.Response;

namespace Category.API.Application.Features.DeleteCategory
{
    public record DeleteCategoryResponse : BaseResponse
    {
        public string DeletedCategoryId { get; init; }

        public DeleteCategoryResponse(Error error = null) : base(error)
        {
        }
    }
}