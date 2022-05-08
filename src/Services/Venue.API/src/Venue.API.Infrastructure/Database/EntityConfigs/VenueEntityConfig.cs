using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venue.API.Application.Database.PersistenceModels;

namespace Venue.API.Infrastructure.Database.EntityConfigs
{
    public class VenueEntityConfig : IEntityTypeConfiguration<VenuePersistenceModel>
    {
        public static VenueEntityConfig Create() => new VenueEntityConfig();

        public void Configure(EntityTypeBuilder<VenuePersistenceModel> builder)
        {
            builder.HasKey(v => v.VenueId);

            builder.HasIndex(v => v.CategoryId);
        }
    }
}