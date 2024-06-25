using BusinessFlow.Domain.SpaceAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class SpacePermissionEntityConfiguration : IEntityTypeConfiguration<SpacePermission>
{
    public void Configure(EntityTypeBuilder<SpacePermission> builder)
    {
        builder.HasKey(x => x.Id);
    }
}