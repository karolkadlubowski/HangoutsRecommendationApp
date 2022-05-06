using Library.Shared.Models.Response;
using Library.Shared.Models.Venue.Dtos;

namespace Venue.API.Application.Features.UpdateVenue
{
    public record UpdateVenueResponse : BaseResponse
    {
        public VenueDto UpdatedVenue { get; init; }

        public UpdateVenueResponse(Error error = null) : base(error)
        {
        }
    }
}