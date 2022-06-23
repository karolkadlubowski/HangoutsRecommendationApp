using Library.Shared.Models.Response;

namespace Identity.API.Application.Features.ChangeUserPassword
{
    public record ChangeUserPasswordResponse : BaseResponse
    {
        public ChangeUserPasswordResponse(Error error = null) : base(error)
        {
        }
    }
}