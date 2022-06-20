using MediatR;

namespace VenueList.API.Application.Handlers.DeleteFavoritesFromVenue
{
    public record DeleteFavoritesFromVenueCommand
    (
        long VenueId
    ) : IRequest<DeleteFavoritesFromVenueResponse>;
}