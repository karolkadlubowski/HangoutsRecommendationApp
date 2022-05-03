using Library.Shared.Models.Response;

namespace AccountDefinition.API.Application.Features.DeleteAccountProviderById
{
    public record DeleteAccountProviderByIdResponse : BaseResponse
    {
        public long DeletedAccountProviderId { get; init; }

        public DeleteAccountProviderByIdResponse(Error error = null) : base(error)
        {
        }
    }
}