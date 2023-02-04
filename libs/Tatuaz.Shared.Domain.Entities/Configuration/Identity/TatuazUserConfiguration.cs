using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Identity;

public class TatuazUserConfiguration : IEntityTypeConfiguration<TatuazUser>
{
    public void Configure(EntityTypeBuilder<TatuazUser> builder)
    {
        builder.ToTable("tatuaz_users", TatuazIdentityConstants.SchemaName);

        builder.Property(x => x.Id).HasMaxLength(320);

        builder.Property(x => x.Username).HasMaxLength(32);

        builder
            .HasMany(x => x.UserRoles)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserEmail)
            .HasConstraintName("fk_tatuaz_user_roles_tatuaz_users_tatuaz_user_id");
    }
}
