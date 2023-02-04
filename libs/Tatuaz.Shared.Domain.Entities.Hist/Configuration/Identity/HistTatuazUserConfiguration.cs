using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Identity;

public class HistTatuazUserConfiguration : IEntityTypeConfiguration<HistTatuazUser>
{
    public void Configure(EntityTypeBuilder<HistTatuazUser> builder)
    {
        builder.ToTable("H_tatuaz_users", HistTatuazIdentityConstants.SchemaName);

        builder.HasKey(x => x.HistId);

        builder.Property(x => x.Id).HasMaxLength(320);

        builder.Property(x => x.Username).HasMaxLength(32);
    }
}
