using Library.Shared.Models.AccountDefinition.Dtos;
using Library.Shared.Models.AccountDefinition.Events;
using Library.Shared.Models.Response;

namespace AccountDefinition.API.Application.Features.AddAccountProvider
{
    public record AddAccountProviderResponse : BaseApiResponse
    {
        public AccountProviderDto AddedAccountProvider { get; init; }
        public AccountProviderAddedEvent Event { get; init; }

        public AddAccountProviderResponse(Error error = null) : base(error)
        {
        }
    }
}