using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Photo;

public class UserPhotoCategoryConfiguration : IEntityTypeConfiguration<UserPhotoCategory>
{
    public void Configure(EntityTypeBuilder<UserPhotoCategory> builder)
    {
        builder.ToTable("user_photo_categories", TatuazPhotoConstants.SchemaName);

        builder.Property(x => x.UserId).HasMaxLength(320);
    }
}
