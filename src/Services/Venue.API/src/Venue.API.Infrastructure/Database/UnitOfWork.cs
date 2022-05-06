using System.Threading.Tasks;
using Venue.API.Application.Database;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Application.Database.Repositories;
using Venue.API.Infrastructure.Database.Repositories;

namespace Venue.API.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VenueDbContext _dbContext;

        public UnitOfWork(VenueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CompleteAsync()
            => await _dbContext.SaveChangesAsync() > 0;

        private IVenueRepository _venueRepository;

        public IVenueRepository VenueRepository
            => _venueRepository ?? new VenueRepository(_dbContext);

        private IGenericRepository<LocationPersistenceModel> _locationRepository;

        public IGenericRepository<LocationPersistenceModel> LocationRepository
            => _locationRepository ?? new GenericRepository<LocationPersistenceModel>(_dbContext);

        private IGenericRepository<LocationCoordinatePersistenceModel> _locationCoordinateRepository;

        public IGenericRepository<LocationCoordinatePersistenceModel> LocationCoordinateRepository
            => _locationCoordinateRepository ?? new GenericRepository<LocationCoordinatePersistenceModel>(_dbContext);

        public void Dispose() => _dbContext.Dispose();
    }
}