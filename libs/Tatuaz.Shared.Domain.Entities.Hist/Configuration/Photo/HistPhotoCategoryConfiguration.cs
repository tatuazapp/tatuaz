using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Photo;

public class HistPhotoCategoryConfiguration : IEntityTypeConfiguration<HistPhotoCategory>
{
    public void Configure(EntityTypeBuilder<HistPhotoCategory> builder)
    {
        builder.ToTable("H_photo_categories", HistTatuazPhotoConstants.SchemaName);

        builder.HasKey(x => x.HistId);
    }
}
