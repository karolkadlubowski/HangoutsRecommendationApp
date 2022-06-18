using MediatR;

namespace VenueList.API.Application.Features.AddVenue
{
    public record AddVenueCommand : IRequest<AddVenueResponse>
    {
        public long VenueId { get; init; }
        public long UserId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryId { get; init; }
        public long? CreatorId { get; init; }
    }
}