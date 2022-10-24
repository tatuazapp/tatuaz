using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Identity;

public class HistTatuazUserClaimConfiguration : IEntityTypeConfiguration<HistTatuazUserClaim>
{
    public void Configure(EntityTypeBuilder<HistTatuazUserClaim> builder)
    {
        builder.ToTable("H_tatuaz_user_claims", HistTatuazIdentityConstants.SchemaName);

        builder.HasKey(x => x.HistId);

        builder.Property(x => x.ClaimType)
            .HasColumnType("character varying(256)")
            .HasMaxLength(256);

        builder.Property(x => x.ClaimValue)
            .HasColumnType("character varying(256)")
            .HasMaxLength(256);
    }
}
