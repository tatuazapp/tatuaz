using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Photo;

public class HistUserPhotoCategoryConfiguration : IEntityTypeConfiguration<HistUserPhotoCategory>
{
    public void Configure(EntityTypeBuilder<HistUserPhotoCategory> builder)
    {
        builder.ToTable("H_user_photo_categories", HistTatuazPhotoConstants.SchemaName);

        builder.HasKey(x => x.HistId);

        builder.Property(x => x.UserId).HasMaxLength(320);
    }
}
