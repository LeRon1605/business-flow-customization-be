using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class SubmissionExecutionPersonInChargeEntityConfiguration : IEntityTypeConfiguration<SubmissionExecutionPersonInCharge>
{
    public void Configure(EntityTypeBuilder<SubmissionExecutionPersonInCharge> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Execution)
            .WithMany(x => x.PersonInCharges)
            .HasForeignKey(x => x.ExecutionId);
    }
}