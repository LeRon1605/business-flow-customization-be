using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class BusinessFlowBlockPersonInChargeSettingEntityTypeConfiguration : IEntityTypeConfiguration<BusinessFlowBlockPersonInChargeSetting>
{
    public void Configure(EntityTypeBuilder<BusinessFlowBlockPersonInChargeSetting> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.BusinessFlowBlock)
            .WithMany(x => x.PersonInChargeSettings)
            .HasForeignKey(x => x.BusinessFlowBlockId);
    }
}