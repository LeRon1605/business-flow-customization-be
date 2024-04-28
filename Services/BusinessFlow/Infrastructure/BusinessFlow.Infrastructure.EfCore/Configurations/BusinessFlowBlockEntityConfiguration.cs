using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class BusinessFlowBlockEntityConfiguration : IEntityTypeConfiguration<BusinessFlowBlock>
{
    public void Configure(EntityTypeBuilder<BusinessFlowBlock> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.BusinessFlowVersion)
            .WithMany(x => x.Blocks)
            .HasForeignKey(x => x.BusinessFlowVersionId);
    }
}