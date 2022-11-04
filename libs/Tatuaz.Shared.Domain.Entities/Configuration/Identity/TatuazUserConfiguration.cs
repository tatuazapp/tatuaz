using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Identity;

public class TatuazUserConfiguration : IEntityTypeConfiguration<TatuazUser>
{
    public void Configure(EntityTypeBuilder<TatuazUser> builder)
    {
        builder.ToTable("tatuaz_users", TatuazIdentityConstants.SchemaName);

        builder
            .Property(x => x.Username)
            .HasMaxLength(32);
        builder
            .Property(x => x.Email)
            .HasMaxLength(256);
        builder
            .Property(x => x.PhoneNumber)
            .HasMaxLength(16);

        builder
            .HasMany(x => x.TatuazUserRoles)
            .WithOne(x => x.TatuazUser)
            .HasForeignKey(x => x.TatuazUserId)
            .HasConstraintName("fk_tatuaz_user_roles_tatuaz_users_tatuaz_user_id");
    }
}