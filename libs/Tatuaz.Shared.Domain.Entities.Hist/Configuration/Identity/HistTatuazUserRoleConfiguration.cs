using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Identity;

public class HistTatuazUserRoleConfiguration : IEntityTypeConfiguration<HistTatuazUserRole>
{
    public void Configure(EntityTypeBuilder<HistTatuazUserRole> builder)
    {
        builder.ToTable("H_tatuaz_user_roles", HistTatuazIdentityConstants.SchemaName);

        builder.HasKey(x => x.HistId);
    }
}
