using Library.Shared.Models.Pagination;
using Library.Shared.Models.Response;
using Library.Shared.Models.Venue.Dtos;

namespace Venue.API.Application.Features.GetVenues
{
    public record GetVenuesResponse : BaseResponse
    {
        public IPagedList<VenueDto> Venues { get; init; } = PagedList<VenueDto>.Empty;
        public PaginationResponseDecorator<VenueDto> Pagination { get; init; }

        public GetVenuesResponse(Error error = null) : base(error)
        {
        }
    }
}