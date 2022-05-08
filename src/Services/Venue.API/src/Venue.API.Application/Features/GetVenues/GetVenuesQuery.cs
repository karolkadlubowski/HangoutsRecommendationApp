using System.Collections.Generic;
using Library.Shared.Models.Pagination;
using MediatR;

namespace Venue.API.Application.Features.GetVenues
{
    public record GetVenuesQuery : PaginationRequestDecorator, IRequest<GetVenuesResponse>
    {
        public IEnumerable<long> LocationsIds { get; init; } = new List<long>();
        public IEnumerable<string> CategoriesIds { get; init; } = new List<string>();
    }
}