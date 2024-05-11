using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class OptionFormElementSettingEntityConfiguration : IEntityTypeConfiguration<OptionFormElementSetting>
{
    public void Configure(EntityTypeBuilder<OptionFormElementSetting> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.FormElement)
            .WithMany(x => x.Options)
            .HasForeignKey(x => x.FormElementId);
    }
}