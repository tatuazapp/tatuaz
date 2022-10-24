using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Identity;

public class TatuazRoleClaimConfiguration : IEntityTypeConfiguration<TatuazRoleClaim>
{
    public void Configure(EntityTypeBuilder<TatuazRoleClaim> builder)
    {
        builder.ToTable("tatuaz_role_claims", TatuazIdentityConstants.SchemaName);

        builder.HasOne<TatuazRole>()
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .HasConstraintName("fk_tatuaz_role_claims_tatuaz_roles_role_id");
    }
}
