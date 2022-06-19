using System;
using System.Threading.Tasks;
using VenueList.API.Application.Features.AddFavorite;
using VenueList.API.Application.Features.DeleteFavorite;

namespace VenueList.API.Application.Abstractions
{
    public interface IFavoriteService : IReadOnlyFavoriteService
    {
        Task<Domain.Entities.Favorite> AddVenueAsync(AddFavoriteCommand command);
        
        Task<string> DeleteFavoriteAsync(DeleteFavoriteCommand command);
    }
}