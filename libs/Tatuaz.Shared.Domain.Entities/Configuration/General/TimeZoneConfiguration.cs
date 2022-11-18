using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.General;

namespace Tatuaz.Shared.Domain.Entities.Configuration.General;

public class TimeZoneConfiguration : IEntityTypeConfiguration<TimeZone>
{
    public void Configure(EntityTypeBuilder<TimeZone> builder)
    {
        builder.ToTable("time_zones", TatuazGeneralConstants.SchemaName);

        builder.Property(x => x.Name).HasMaxLength(64);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Description).HasMaxLength(256);
    }
}
