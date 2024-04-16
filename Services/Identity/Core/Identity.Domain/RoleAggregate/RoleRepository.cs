using BuildingBlocks.Domain.Repositories;
using ApplicationRole = Identity.Domain.RoleAggregate.Entities.ApplicationRole;

namespace Identity.Domain.RoleAggregate;

public interface IRoleRepository : IRepository<ApplicationRole, string>
{
    Task<ApplicationRole?> FindByNameAsync(string name);
    
    Task<IList<ApplicationRole>> FindByUserAsync(string userId, int tenantId);
}