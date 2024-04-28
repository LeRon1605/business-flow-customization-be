using BusinessFlow.Domain.SpaceAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessFlow.Infrastructure.EfCore.Configurations;

public class SpaceMemberEntityConfiguration : IEntityTypeConfiguration<SpaceMember>
{
    public void Configure(EntityTypeBuilder<SpaceMember> builder)
    {
        builder.HasKey(x => new { x.UserId, x.RoleId });
        
        builder.HasOne(x => x.Space)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.SpaceId);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.RoleId);
    }
}