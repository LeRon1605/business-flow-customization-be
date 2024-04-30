using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class BusinessFlowBranchEntityConfiguration : IEntityTypeConfiguration<BusinessFlowBranch>
{
    public void Configure(EntityTypeBuilder<BusinessFlowBranch> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.FromBlock)
            .WithMany(x => x.FromBranches)
            .HasForeignKey(x => x.FromBlockId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.ToBlock)
            .WithMany(x => x.ToBranches)
            .HasForeignKey(x => x.ToBlockId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.OutCome)
            .WithMany(x => x.Branches)
            .HasForeignKey(x => x.OutComeId);
    }
}