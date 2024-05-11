using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class FormEntityConfiguration : IEntityTypeConfiguration<Form>
{
    public void Configure(EntityTypeBuilder<Form> builder)
    {
        builder.HasKey(x => x.Id);
    }
}