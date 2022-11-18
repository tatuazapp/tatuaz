using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.General;

public class HistCityConfiguration : IEntityTypeConfiguration<HistCity>
{
    public void Configure(EntityTypeBuilder<HistCity> builder)
    {
        builder.ToTable("H_cities", HistTatuazGeneralConstants.SchemaName);

        builder.Property(x => x.Name).HasMaxLength(128);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Country).HasMaxLength(128);

        builder.Property(x => x.Location).HasColumnType("geography (point)");

        builder.HasIndex(x => x.Location).IsUnique();
    }
}
