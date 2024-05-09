using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class SubmissionNumberValueEntityConfiguration : IEntityTypeConfiguration<SubmissionNumberValue>
{
    public void Configure(EntityTypeBuilder<SubmissionNumberValue> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Submission)
            .WithMany(x => x.NumberFields)
            .HasForeignKey(x => x.SubmissionId);
    }
}