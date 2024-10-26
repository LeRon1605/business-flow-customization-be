﻿using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class BusinessFlowOutComeEntityConfiguration : IEntityTypeConfiguration<BusinessFlowOutCome>
{
    public void Configure(EntityTypeBuilder<BusinessFlowOutCome> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.BusinessFlowBlock)
            .WithMany(x => x.OutComes)
            .HasForeignKey(x => x.BusinessFlowBlockId);
    }
}