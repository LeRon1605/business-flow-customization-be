using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class SubmissionEntityConfiguration : IEntityTypeConfiguration<FormSubmission>
{
    public void Configure(EntityTypeBuilder<FormSubmission> builder)
    {
        builder.HasKey(x => x.Id);
    }
}