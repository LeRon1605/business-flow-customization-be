using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.SpaceBusinessFlowAggregate.Entities;

namespace Submission.Infrastructure.EfCore.Configurations;

public class SpaceBusinessFlowEntityConfiguration : IEntityTypeConfiguration<SpaceBusinessFlow>
{
    public void Configure(EntityTypeBuilder<SpaceBusinessFlow> builder)
    {
        builder.HasKey(x => x.Id);
    }
}