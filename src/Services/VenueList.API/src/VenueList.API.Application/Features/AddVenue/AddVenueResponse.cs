using Library.Shared.Models.Response;
using Library.Shared.Models.VenueList.Dtos;

namespace VenueList.API.Application.Features.AddVenue
{
    public record AddVenueResponse : BaseResponse
    {
        public VenueDto AddedVenue { get; init; }

        public AddVenueResponse(Error error = null) : base(error)
        {
        }
    }
}