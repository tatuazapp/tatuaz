using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Identity;

public class HistTatuazRoleClaimConfiguration : IEntityTypeConfiguration<HistTatuazRoleClaim>
{
    public void Configure(EntityTypeBuilder<HistTatuazRoleClaim> builder)
    {
        builder.ToTable("H_tatuaz_role_claims", HistTatuazIdentityConstants.SchemaName);

        builder.HasKey(x => x.HistId);
    }
}
