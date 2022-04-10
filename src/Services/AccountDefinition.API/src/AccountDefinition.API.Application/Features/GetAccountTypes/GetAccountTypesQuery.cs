using MediatR;

namespace AccountDefinition.API.Application.Features.GetAccountTypes
{
    public record GetAccountTypesQuery : IRequest<GetAccountTypesResponse>;
}