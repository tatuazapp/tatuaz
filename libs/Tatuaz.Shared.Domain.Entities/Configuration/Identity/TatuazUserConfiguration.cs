using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Identity;

public class TatuazUserConfiguration : IEntityTypeConfiguration<TatuazUser>
{
    public void Configure(EntityTypeBuilder<TatuazUser> builder)
    {
        builder.ToTable("tatuaz_users", TatuazIdentityConstants.SchemaName);
    }
}
