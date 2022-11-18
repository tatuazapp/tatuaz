using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.General;

public class HistTimeZoneConfiguration : IEntityTypeConfiguration<HistTimeZone>
{
    public void Configure(EntityTypeBuilder<HistTimeZone> builder)
    {
        builder.ToTable("H_time_zones", HistTatuazGeneralConstants.SchemaName);

        builder.Property(x => x.Name).HasMaxLength(64);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Description).HasMaxLength(256);
    }
}
