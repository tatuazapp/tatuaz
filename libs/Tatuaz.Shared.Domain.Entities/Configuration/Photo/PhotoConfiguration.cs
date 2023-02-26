using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Photo;

public class PhotoConfiguration : IEntityTypeConfiguration<Models.Photo.Photo>
{
    public void Configure(EntityTypeBuilder<Models.Photo.Photo> builder)
    {
        builder.ToTable("photos", TatuazPhotoConstants.SchemaName);
    }
}
