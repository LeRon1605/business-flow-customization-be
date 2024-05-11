using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class FormVersionEntityConfiguration : IEntityTypeConfiguration<FormVersion>
{
    public void Configure(EntityTypeBuilder<FormVersion> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Form)
            .WithMany(x => x.Versions)
            .HasForeignKey(x => x.FormId);
    }
}