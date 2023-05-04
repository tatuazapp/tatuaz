using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Post;

public class InitialPostConfiguration : IEntityTypeConfiguration<InitialPost>
{
    public void Configure(EntityTypeBuilder<InitialPost> builder)
    {
        builder.ToTable("initial_posts", TatuazPostConstants.SchemaName);
    }
}
