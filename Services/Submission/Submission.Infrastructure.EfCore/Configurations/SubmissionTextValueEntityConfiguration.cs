using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class SubmissionTextValueEntityConfiguration : IEntityTypeConfiguration<SubmissionTextValue>
{
    public void Configure(EntityTypeBuilder<SubmissionTextValue> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Submission)
            .WithMany(x => x.TextFields)
            .HasForeignKey(x => x.SubmissionId);
        
        builder.HasOne(x => x.Element)
            .WithMany()
            .HasForeignKey(x => x.ElementId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}