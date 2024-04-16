using System.Linq.Expressions;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Infrastructure.EfCore;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.UserAggregate.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.EfCore;

public class AppIdentityDbContext : IdentityDbContext<ApplicationUser
    , ApplicationRole
    , string
    , IdentityUserClaim<string>
    , ApplicationUserInRole
    , IdentityUserLogin<string>
    , IdentityRoleClaim<string>
    , IdentityUserToken<string>>
{
    public AppIdentityDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppIdentityDbContext).Assembly);
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IHasSoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "p");
                var deletedCheck =
                    Expression.Lambda(
                        Expression.Equal(Expression.Property(parameter, nameof(IHasSoftDelete.IsDeleted)), 
                            Expression.Constant(false)),
                        parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
            }
        }
    }
}