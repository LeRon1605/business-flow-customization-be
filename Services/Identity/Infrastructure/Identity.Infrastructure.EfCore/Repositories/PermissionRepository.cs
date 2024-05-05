using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Identity.Domain.PermissionAggregate;
using Identity.Domain.PermissionAggregate.Entities;
using Identity.Domain.PermissionAggregate.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.EfCore.Repositories;

public class PermissionRepository : EfCoreRepository<ApplicationPermission>, IPermissionRepository
{
    public PermissionRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }

    public async Task<IList<ApplicationPermission>> FilterAsync(string name)
    {
        return await GetQueryable(new PermissionByNameSpecification(name)).OrderBy(x => x.Name).ToListAsync();
    }
}