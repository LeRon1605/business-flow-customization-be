using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class SubmissionDateValueEntityConfiguration : IEntityTypeConfiguration<SubmissionDateValue>
{
    public void Configure(EntityTypeBuilder<SubmissionDateValue> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Submission)
            .WithMany(x => x.DateFields)
            .HasForeignKey(x => x.SubmissionId);
        
        builder.HasOne(x => x.Element)
            .WithMany()
            .HasForeignKey(x => x.ElementId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}