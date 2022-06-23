using System.Threading;
using System.Threading.Tasks;
using Identity.API.Application.Database.PersistenceModels;
using Identity.API.Infrastructure.Database.EntityConfigs;
using Library.Database;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Identity.API.Infrastructure.Database
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<UserPersistenceModel> Users { get; protected set; }

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

            modelBuilder.HasDefaultSchema("Identity");

            UserEntityConfig.Create().Configure(modelBuilder.Entity<UserPersistenceModel>());
        }
    }
}