using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class FormElementSettingEntityConfiguration : IEntityTypeConfiguration<FormElementSetting>
{
    public void Configure(EntityTypeBuilder<FormElementSetting> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.FormElement)
            .WithMany(x => x.Settings)
            .HasForeignKey(x => x.FormElementId);
    }
}