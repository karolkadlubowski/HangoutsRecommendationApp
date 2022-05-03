using Library.Shared.Models.Response;
using Library.Shared.Models.Venue.Dtos;

namespace Venue.API.Application.Features.GetVenue
{
    public record GetVenueResponse : BaseResponse
    {
        public VenueDto Venue { get; init; }

        public GetVenueResponse(Error error = null) : base(error)
        {
        }
    }
}