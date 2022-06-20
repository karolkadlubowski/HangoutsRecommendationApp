using System;
using System.Threading.Tasks;
using VenueList.API.Application.Features.AddVenueToFavorites;
using VenueList.API.Application.Features.DeleteFavorite;

namespace VenueList.API.Application.Abstractions
{
    public interface IFavoriteService : IReadOnlyFavoriteService
    {
        Task<Domain.Entities.Favorite> AddVenueToFavoritesAsync(AddVenueToFavoritesCommand command);

        Task<long> DeleteFavoriteAsync(DeleteFavoriteCommand command, long userId);
    }
}