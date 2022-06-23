using Identity.API.Application.Database.PersistenceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.API.Infrastructure.Database.EntityConfigs
{
    public class UserEntityConfig : IEntityTypeConfiguration<UserPersistenceModel>
    {
        public static UserEntityConfig Create() => new();

        public void Configure(EntityTypeBuilder<UserPersistenceModel> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.HasIndex(u => u.Email);
        }
    }
}