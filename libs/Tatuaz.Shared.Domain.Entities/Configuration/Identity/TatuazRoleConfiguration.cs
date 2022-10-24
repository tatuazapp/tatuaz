using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Identity;

public class TatuazRoleConfiguration : IEntityTypeConfiguration<TatuazRole>
{
    public void Configure(EntityTypeBuilder<TatuazRole> builder)
    {
        builder.ToTable("tatuaz_roles", TatuazIdentityConstants.SchemaName);
    }
}
