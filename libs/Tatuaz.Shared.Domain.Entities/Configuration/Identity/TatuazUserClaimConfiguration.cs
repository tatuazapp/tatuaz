using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Identity;

public class TatuazUserClaimConfiguration : IEntityTypeConfiguration<TatuazUserClaim>
{
    public void Configure(EntityTypeBuilder<TatuazUserClaim> builder)
    {
        builder.ToTable("tatuaz_user_claims", TatuazIdentityConstants.SchemaName);

        builder.HasOne<TatuazUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_tatuaz_user_claims_tatuaz_users_user_id");
    }
}
