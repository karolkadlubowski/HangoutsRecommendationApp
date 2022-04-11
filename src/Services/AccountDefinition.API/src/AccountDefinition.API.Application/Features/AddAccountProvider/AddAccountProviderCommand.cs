using MediatR;

namespace AccountDefinition.API.Application.Features.AddAccountProvider
{
    public record AddAccountProviderCommand : IRequest<AddAccountProviderResponse>
    {
        public string Provider { get; init; }
    }
}