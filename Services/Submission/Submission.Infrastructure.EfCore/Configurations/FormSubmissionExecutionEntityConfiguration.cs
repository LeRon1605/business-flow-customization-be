using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class FormSubmissionExecutionEntityConfiguration : IEntityTypeConfiguration<FormSubmissionExecution>
{
    public void Configure(EntityTypeBuilder<FormSubmissionExecution> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasOne(x => x.FormSubmission)
            .WithOne(x => x.Execution)
            .HasForeignKey<FormSubmissionExecution>(x => x.FormSubmissionId);
    }
}