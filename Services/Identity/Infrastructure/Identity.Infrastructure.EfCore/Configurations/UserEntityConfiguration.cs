using Identity.Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.EfCore.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FullName)
               .IsRequired();

        builder.Property(x => x.AvatarUrl)
               .IsRequired();

        builder.Property(x => x.ExternalId)
               .IsRequired(false);
    }
}