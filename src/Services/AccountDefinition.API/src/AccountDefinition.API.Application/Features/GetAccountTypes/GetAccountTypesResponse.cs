using System.Collections.Generic;
using System.Collections.Immutable;
using Library.Shared.Models.AccountDefinition.Dtos;
using Library.Shared.Models.Response;

namespace AccountDefinition.API.Application.Features.GetAccountTypes
{
    public record GetAccountTypesResponse : BaseResponse
    {
        public IReadOnlyList<AccountTypeDto> AccountTypes { get; init; } = ImmutableList<AccountTypeDto>.Empty;

        public GetAccountTypesResponse(Error error = null) : base(error)
        {
        }
    }
}