using System;
using System.Threading.Tasks;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Application.Database.Repositories;

namespace Venue.API.Application.Database
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CompleteAsync();

        IVenueRepository VenueRepository { get; }
        IGenericRepository<LocationPersistenceModel> LocationRepository { get; }
        IGenericRepository<LocationCoordinatePersistenceModel> LocationCoordinateRepository { get; }
    }
}