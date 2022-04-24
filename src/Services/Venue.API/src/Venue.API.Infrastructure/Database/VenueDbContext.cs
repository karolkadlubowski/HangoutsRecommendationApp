using System.Linq;
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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified)
                .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                var entity = modifiedEntry as BasePersistenceModel;
                if (entity is not null)
                    entity.UpdateNow();
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Venue");

            VenueEntityConfig.Create().Configure(modelBuilder.Entity<VenuePersistenceModel>());
        }
    }
}