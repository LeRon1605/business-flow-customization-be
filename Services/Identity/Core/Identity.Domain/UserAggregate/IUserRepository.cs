using BuildingBlocks.Domain.Repositories;
using Identity.Domain.RoleAggregate.Entities;
using ApplicationUser = Identity.Domain.UserAggregate.Entities.ApplicationUser;

namespace Identity.Domain.UserAggregate;

public interface IUserRepository : IRepository<ApplicationUser, string>
{
    Task<ApplicationUser?> FindByUserNameOrEmailAsync(string username, string email);

    Task<ApplicationUser?> FindByUserNameOrEmailAndRoles(string username, string email, IEnumerable<string> roles);
    
    Task<ApplicationUser?> FindByRefreshTokenAsync(string refreshToken);

    Task<IList<string>> GetRolesAsync(string id, int tenantId);
    
    Task<IList<TOut>> FindByTenantAsync<TOut>(int tenantId, IProjection<ApplicationUser, string , TOut> projection);
    
    Task<string> GetRoleInTenant(string id, int tenantId);
}