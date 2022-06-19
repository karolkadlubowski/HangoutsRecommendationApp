using MediatR;

namespace VenueList.API.Application.Features.DeleteFavorite
{
    public record DeleteFavoriteCommand : IRequest<DeleteFavoriteResponse>
    {
        public string FavoriteId { get; init; }

    }
}