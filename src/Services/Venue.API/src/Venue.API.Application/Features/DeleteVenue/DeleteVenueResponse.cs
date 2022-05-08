using Library.Shared.Models.Response;

namespace Venue.API.Application.Features.DeleteVenue
{
    public record DeleteVenueResponse : BaseResponse
    {
        public long DeletedVenueId { get; init; }
        public long DeletedLocationId { get; init; }

        public DeleteVenueResponse(Error error = null) : base(error)
        {
        }
    }
}