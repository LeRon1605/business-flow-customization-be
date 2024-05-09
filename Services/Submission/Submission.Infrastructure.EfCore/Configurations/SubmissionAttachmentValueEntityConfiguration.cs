using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class SubmissionAttachmentValueEntityConfiguration : IEntityTypeConfiguration<SubmissionAttachmentValue>
{
    public void Configure(EntityTypeBuilder<SubmissionAttachmentValue> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Field)
            .WithMany(x => x.Values)
            .HasForeignKey(x => x.SubmissionFieldId);
    }
}