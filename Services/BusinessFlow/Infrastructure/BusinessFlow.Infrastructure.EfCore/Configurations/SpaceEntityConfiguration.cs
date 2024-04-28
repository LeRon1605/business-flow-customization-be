using BusinessFlow.Domain.SpaceAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class SpaceEntityConfiguration : IEntityTypeConfiguration<Space>
{
    public void Configure(EntityTypeBuilder<Space> builder)
    {
        builder.HasKey(x => x.Id);
    }
}