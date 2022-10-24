using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Identity;

public class TatuazUserRoleConfiguration : IEntityTypeConfiguration<TatuazUserRole>
{
    public void Configure(EntityTypeBuilder<TatuazUserRole> builder)
    {
        builder.ToTable("tatuaz_user_roles", TatuazIdentityConstants.SchemaName);

        builder.HasOne<TatuazUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_tatuaz_user_roles_tatuaz_users_user_id");

        builder.HasOne<TatuazRole>()
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .HasConstraintName("fk_tatuaz_user_roles_tatuaz_roles_role_id");
    }
}
