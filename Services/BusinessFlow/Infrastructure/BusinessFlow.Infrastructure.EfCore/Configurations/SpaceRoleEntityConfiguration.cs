using BusinessFlow.Domain.SpaceAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class SpaceRoleEntityConfiguration : IEntityTypeConfiguration<SpaceRole>
{
    public void Configure(EntityTypeBuilder<SpaceRole> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(r => r.Permissions)
            .WithOne(p => p.Role)
            .HasForeignKey(p => p.RoleId);
    }
}