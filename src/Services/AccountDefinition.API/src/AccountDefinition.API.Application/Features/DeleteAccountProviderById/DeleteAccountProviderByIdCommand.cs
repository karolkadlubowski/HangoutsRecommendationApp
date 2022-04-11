using MediatR;

namespace AccountDefinition.API.Application.Features.DeleteAccountProviderById
{
    public record DeleteAccountProviderByIdCommand : IRequest<DeleteAccountProviderByIdResponse>
    {
        public long AccountProviderId { get; init; }
    }
}