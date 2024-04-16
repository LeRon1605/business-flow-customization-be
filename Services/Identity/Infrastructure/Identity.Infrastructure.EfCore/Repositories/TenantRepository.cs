using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.EfCore.Repositories;

public class TenantRepository : EfCoreRepository<Tenant, int>, ITenantRepository
{
    public TenantRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public async Task<IList<Tenant>> FindByUserAsync(string userId)
    {
        var userInTenantQueryable = DbContext.Set<UserInTenant>().Where(x => x.UserId == userId);
        
        return await DbSet.Where(x => userInTenantQueryable.Any(y => y.TenantId == x.Id))
                    .ToListAsync();
    }
}