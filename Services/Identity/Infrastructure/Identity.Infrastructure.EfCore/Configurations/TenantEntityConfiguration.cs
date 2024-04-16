using Identity.Domain.TenantAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.EfCore.Configurations;

public class TenantEntityConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
               .IsRequired();

        builder.Property(x => x.AvatarUrl)
               .IsRequired();
    }
}