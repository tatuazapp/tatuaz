using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Photo;

public class HistCategoryConfiguration : IEntityTypeConfiguration<HistCategory>
{
    public void Configure(EntityTypeBuilder<HistCategory> builder)
    {
        builder.ToTable("H_categories", HistTatuazPhotoConstants.SchemaName);

        builder.HasKey(x => x.HistId);

        builder.Property(x => x.Title).HasMaxLength(64);

        builder.Property(x => x.ImageUri).HasMaxLength(256);
    }
}
