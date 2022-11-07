using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Identity;

public class HistTatuazRoleConfiguration : IEntityTypeConfiguration<HistTatuazRole>
{
    public void Configure(EntityTypeBuilder<HistTatuazRole> builder)
    {
        builder.ToTable("H_tatuaz_roles", HistTatuazIdentityConstants.SchemaName);

        builder.HasKey(x => x.HistId);

        builder.Property(x => x.Name).HasMaxLength(128);
    }
}
