using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Identity;

public class TatuazRoleConfiguration : IEntityTypeConfiguration<TatuazRole>
{
    public void Configure(EntityTypeBuilder<TatuazRole> builder)
    {
        builder.ToTable("tatuaz_roles", TatuazIdentityConstants.SchemaName);

        builder
            .Property(x => x.Name)
            .HasMaxLength(128);

        builder
            .HasMany(x => x.TatuazUserRoles)
            .WithOne(x => x.TatuazRole)
            .HasForeignKey(x => x.TatuazRoleId)
            .HasConstraintName("fk_tatuaz_user_roles_tatuaz_roles_tatuaz_role_id");
    }
}
