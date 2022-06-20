using Library.Shared.Models.Response;

namespace Identity.API.Application.Features.ChangeUserEmail
{
    public record ChangeUserEmailResponse : BaseResponse
    {
        public ChangeUserEmailResponse(Error error = null) : base(error)
        {
        }
    }
}