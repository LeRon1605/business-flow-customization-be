using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class FormElementEntityConfiguration : IEntityTypeConfiguration<FormElement>
{
    public void Configure(EntityTypeBuilder<FormElement> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.FormVersion)
            .WithMany(x => x.Elements)
            .HasForeignKey(x => x.FormVersionId);
    }
}