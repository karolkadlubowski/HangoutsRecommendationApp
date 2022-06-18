using System.Threading.Tasks;
using VenueList.API.Application.Features.AddFavorite;

namespace VenueList.API.Application.Abstractions
{
    public interface IFavoriteService : IReadOnlyFavoriteService
    {
        Task<Domain.Entities.Favorite> AddVenueAsync(AddFavoriteCommand command);

    }
}