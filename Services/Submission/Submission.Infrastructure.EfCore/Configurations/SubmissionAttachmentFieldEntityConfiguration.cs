using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class SubmissionAttachmentFieldEntityConfiguration : IEntityTypeConfiguration<SubmissionAttachmentField>
{
    public void Configure(EntityTypeBuilder<SubmissionAttachmentField> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Submission)
            .WithMany(x => x.AttachmentFields)
            .HasForeignKey(x => x.SubmissionId);
    }
}