using Library.Shared.Models.Response;
using Library.Shared.Models.Venue.Dtos;

namespace Venue.API.Application.Features.CreateVenue
{
    public record CreateVenueResponse : BaseResponse
    {
        public VenueDto CreatedVenue { get; init; }

        public CreateVenueResponse(Error error = null) : base(error)
        {
        }
    }
}