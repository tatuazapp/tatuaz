using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.General;

namespace Tatuaz.Shared.Domain.Entities.Configuration.General;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("cities", TatuazGeneralConstants.SchemaName);

        builder.Property(x => x.Name).HasMaxLength(128);

        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Country).HasMaxLength(128);

        builder.Property(x => x.Location).HasColumnType("geography (point)");

        builder.HasIndex(x => x.Location).IsUnique();
    }
}
