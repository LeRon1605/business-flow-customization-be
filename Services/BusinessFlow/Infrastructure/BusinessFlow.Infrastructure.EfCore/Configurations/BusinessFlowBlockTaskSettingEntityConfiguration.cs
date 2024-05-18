using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class BusinessFlowBlockTaskSettingEntityConfiguration : IEntityTypeConfiguration<BusinessFlowBlockTaskSetting>
{
    public void Configure(EntityTypeBuilder<BusinessFlowBlockTaskSetting> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.BusinessFlowBlock)
            .WithMany(x => x.TaskSettings)
            .HasForeignKey(x => x.BusinessFlowBlockId);
    }
}