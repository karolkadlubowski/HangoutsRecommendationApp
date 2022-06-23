using MediatR;

namespace VenueList.API.Application.Features.AddVenueToFavorites
{
    public record AddVenueToFavoritesCommand : IRequest<AddVenueToFavoritesResponse>
    {
        public long VenueId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string CategoryName { get; init; }
        public long? CreatorId { get; init; }
    }
}