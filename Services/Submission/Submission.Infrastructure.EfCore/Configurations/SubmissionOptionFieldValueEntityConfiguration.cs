using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class SubmissionOptionFieldValueEntityConfiguration : IEntityTypeConfiguration<SubmissionOptionFieldValue>
{
    public void Configure(EntityTypeBuilder<SubmissionOptionFieldValue> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Field)
            .WithMany(x => x.Values)
            .HasForeignKey(x => x.SubmissionFieldId);

        builder.HasOne(x => x.Option)
            .WithMany()
            .HasForeignKey(x => x.OptionId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}