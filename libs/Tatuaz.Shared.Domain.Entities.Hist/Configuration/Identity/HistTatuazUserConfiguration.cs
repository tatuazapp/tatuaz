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

        builder.Property(x => x.Username).HasMaxLength(128);
        builder.Property(x => x.Email).HasMaxLength(256);
        builder.Property(x => x.PhoneNumber).HasMaxLength(64);
    }
}
