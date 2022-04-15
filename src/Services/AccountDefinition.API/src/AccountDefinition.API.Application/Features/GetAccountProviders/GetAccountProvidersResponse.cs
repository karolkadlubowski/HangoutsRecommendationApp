using System.Collections.Generic;
using System.Collections.Immutable;
using Library.Shared.Models.AccountDefinition.Dtos;
using Library.Shared.Models.Response;

namespace AccountDefinition.API.Application.Features.GetAccountProviders
{
    public record GetAccountProvidersResponse : BaseApiResponse
    {
        public IReadOnlyList<AccountProviderDto> AccountProviders { get; init; } = ImmutableList<AccountProviderDto>.Empty;

        public GetAccountProvidersResponse(Error error = null) : base(error)
        {
        }
    }
}