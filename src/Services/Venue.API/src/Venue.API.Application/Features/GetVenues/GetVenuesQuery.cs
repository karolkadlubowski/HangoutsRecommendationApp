using System.Collections.Generic;
using System.Collections.Immutable;
using Library.Shared.Models.Pagination;
using MediatR;

namespace Venue.API.Application.Features.GetVenues
{
    public record GetVenuesQuery : PaginationRequestDecorator, IRequest<GetVenuesResponse>
    {
        public IEnumerable<long> LocationsIds { get; init; } = ImmutableList<long>.Empty;
    }
}