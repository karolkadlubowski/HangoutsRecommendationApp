using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Venue.API.Application.Database.PersistenceModels;

namespace Venue.API.Infrastructure.Database.EntityConfigs
{
    public class LocationCoordinateEntityConfig : IEntityTypeConfiguration<LocationCoordinatePersistenceModel>
    {
        public static LocationCoordinateEntityConfig Create() => new LocationCoordinateEntityConfig();

        public void Configure(EntityTypeBuilder<LocationCoordinatePersistenceModel> builder)
        {
            builder.HasKey(lc => lc.LocationCoordinateId);

            builder
                .HasOne(lc => lc.Location)
                .WithOne(l => l.LocationCoordinate)
                .HasForeignKey<LocationCoordinatePersistenceModel>(lc => lc.LocationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}