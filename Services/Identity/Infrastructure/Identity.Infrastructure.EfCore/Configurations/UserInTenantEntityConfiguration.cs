using Identity.Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.EfCore.Configurations;

public class UserInTenantEntityConfiguration : IEntityTypeConfiguration<UserInTenant>
{
    public void Configure(EntityTypeBuilder<UserInTenant> builder)
    {
        builder.HasKey(x => new { x.TenantId, x.UserId });

        builder.HasOne(x => x.User)
               .WithMany(x => x.Tenants)
               .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Tenant)
               .WithMany(x => x.Users)
               .HasForeignKey(x => x.TenantId);
    }
}