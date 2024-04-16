using Identity.Domain.PermissionAggregate.Entities;
using Identity.Domain.RoleAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.EfCore.Configurations;

public class PermissionEntityConfiguration : IEntityTypeConfiguration<ApplicationPermission>
{
    public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name)
               .IsUnique(true);
        
        builder.Property(x => x.Name)
               .IsRequired(true);
        
        builder.HasMany<RolePermission>()
               .WithOne()
               .HasForeignKey(x => x.PermissionId);
    }
}