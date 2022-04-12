using MediatR;

namespace AccountDefinition.API.Application.Features.GetAccountProviders
{
    public record GetAccountProvidersQuery : IRequest<GetAccountProvidersResponse>;
}