using MediatR;

namespace VenueList.API.Application.Features.DeleteFavorite
{
    public record DeleteFavoriteCommand : IRequest<DeleteFavoriteResponse>
    {
        public long VenueId { get; init; }
    }
}