using System.Threading;
using System.Threading.Tasks;
using Library.Database;
using Microsoft.EntityFrameworkCore;
using Venue.API.Application.Database.PersistenceModels;
using Venue.API.Infrastructure.Database.EntityConfigs;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Venue.API.Infrastructure.Database
{
    public class VenueDbContext : DbContext
    {
        public VenueDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<VenuePersistenceModel> Venues { get; protected set; }
        public virtual DbSet<LocationPersistenceModel> Locations { get; protected set; }
        public virtual DbSet<LocationCoordinatePersistenceModel> LocationCoordinates { get; protected set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BasePersistenceModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.UpdateNow();
                        break;
                    case EntityState.Deleted:
                        entry.Entity.Delete();
                        entry.Entity.UpdateNow();
                        entry.State = EntityState.Modified;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Venue");

            VenueEntityConfig.Create().Configure(modelBuilder.Entity<VenuePersistenceModel>());
            LocationEntityConfig.Create().Configure(modelBuilder.Entity<LocationPersistenceModel>());
            LocationCoordinateEntityConfig.Create().Configure(modelBuilder.Entity<LocationCoordinatePersistenceModel>());
        }
    }
}