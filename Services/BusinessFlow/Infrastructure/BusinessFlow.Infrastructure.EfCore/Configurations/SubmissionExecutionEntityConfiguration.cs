using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class SubmissionExecutionEntityConfiguration : IEntityTypeConfiguration<SubmissionExecution>
{
    public void Configure(EntityTypeBuilder<SubmissionExecution> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.BusinessFlowBlock)
            .WithMany(x => x.Executions)
            .HasForeignKey(x => x.BusinessFlowBlockId);

        builder.HasOne(x => x.OutCome)
            .WithMany(x => x.Executions)
            .HasForeignKey(x => x.OutComeId);
    }
}