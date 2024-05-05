using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Identity.Domain.RoleAggregate;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.EfCore.Repositories;

public class RoleRepository : EfCoreRepository<ApplicationRole, string>, IRoleRepository
{
    public RoleRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
        AddInclude(x => x.Permissions);
    }
    
    public Task<ApplicationRole?> FindByNameAsync(string name)
    {
        return GetQueryable().FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<IList<ApplicationRole>> FindByUserAsync(string userId, int tenantId)
    {
        var roleQueryable = DbContext.Set<ApplicationUserInRole>()
            .Where(x => x.UserId == userId && x.TenantId == tenantId);
        return await GetQueryable().Where(x => roleQueryable.Any(y => y.RoleId == x.Id)).ToListAsync();
    }
}