using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Post;

public class HistInitialPostConfiguration : IEntityTypeConfiguration<HistInitialPost>
{
    public void Configure(EntityTypeBuilder<HistInitialPost> builder)
    {
        builder.ToTable("H_initial_posts", HistTatuazPostConstants.SchemaName);

        builder.HasKey(x => x.HistId);
    }
}
