using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venue.API.Application.Database.PersistenceModels;

namespace Venue.API.Infrastructure.Database.EntityConfigs
{
    public class LocationEntityConfig : IEntityTypeConfiguration<LocationPersistenceModel>
    {
        public static LocationEntityConfig Create() => new LocationEntityConfig();


        public void Configure(EntityTypeBuilder<LocationPersistenceModel> builder)
        {
            builder.HasKey(l => l.LocationId);

            builder
                .HasOne(l => l.Venue)
                .WithOne(v => v.Location)
                .HasForeignKey<LocationPersistenceModel>(l => l.VenueId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}