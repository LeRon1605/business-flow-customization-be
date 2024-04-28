using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class BusinessFlowVersionEntityConfiguration : IEntityTypeConfiguration<BusinessFlowVersion>
{
    public void Configure(EntityTypeBuilder<BusinessFlowVersion> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Space)
            .WithMany(x => x.BusinessFlowVersions)
            .HasForeignKey(x => x.SpaceId);
    }
}