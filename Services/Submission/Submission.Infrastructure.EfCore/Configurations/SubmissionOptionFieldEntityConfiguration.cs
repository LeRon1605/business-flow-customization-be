using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class SubmissionOptionFieldEntityConfiguration : IEntityTypeConfiguration<SubmissionOptionField>
{
    public void Configure(EntityTypeBuilder<SubmissionOptionField> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Submission)
            .WithMany(x => x.OptionFields)
            .HasForeignKey(x => x.SubmissionId);
        
        builder.HasOne(x => x.Element)
            .WithMany()
            .HasForeignKey(x => x.ElementId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}