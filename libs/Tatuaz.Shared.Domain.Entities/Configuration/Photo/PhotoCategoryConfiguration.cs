using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Photo;

public class PhotoCategoryConfiguration : IEntityTypeConfiguration<PhotoCategory>
{
    public void Configure(EntityTypeBuilder<PhotoCategory> builder)
    {
        builder.ToTable("photo_categories", TatuazPhotoConstants.SchemaName);
    }
}
