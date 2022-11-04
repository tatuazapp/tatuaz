using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Identity;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Identity;

public class TatuazUserRoleConfiguration : IEntityTypeConfiguration<TatuazUserRole>
{
    public void Configure(EntityTypeBuilder<TatuazUserRole> builder)
    {
        builder.ToTable("tatuaz_user_roles", TatuazIdentityConstants.SchemaName);
    }
}