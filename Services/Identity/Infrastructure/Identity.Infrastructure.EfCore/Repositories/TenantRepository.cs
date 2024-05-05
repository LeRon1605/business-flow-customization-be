using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.EfCore.Repositories;

public class TenantRepository : EfCoreRepository<Tenant, int>, ITenantRepository
{
    public TenantRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
        AddInclude(x => x.Invitations);
        AddInclude(x => x.Users);
    }

    public async Task<IList<Tenant>> FindByUserAsync(string userId)
    {
        var userInTenantQueryable = DbContext.Set<UserInTenant>().Where(x => x.UserId == userId);
        
        return await GetQueryable()
            .Where(x => userInTenantQueryable.Any(y => y.TenantId == x.Id))
            .ToListAsync();
    }

    public Task<Tenant?> FindByInvitationTokenAsync(string token)
    {
        return GetQueryable()
            .Where(x => x.Invitations.Any(i => i.Token == token))
            .FirstOrDefaultAsync();
    }
}