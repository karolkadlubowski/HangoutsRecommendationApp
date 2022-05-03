using System.Threading.Tasks;
using Venue.API.Application.Database;
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

        public void Dispose() => _dbContext.Dispose();
    }
}