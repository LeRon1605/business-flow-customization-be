using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class SubmissionExecutionTaskEntityConfiguration : IEntityTypeConfiguration<SubmissionExecutionTask>
{
    public void Configure(EntityTypeBuilder<SubmissionExecutionTask> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Execution)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.ExecutionId);
    }
}