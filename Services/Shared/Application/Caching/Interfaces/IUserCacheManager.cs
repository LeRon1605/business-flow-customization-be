using BuildingBlocks.Application.Caching.Interfaces;

namespace Application.Caching.Interfaces;

public interface IUserCacheManager : ICachingManager
{
    Task<IEnumerable<string>?> GetRolesAsync(string id, int tenantId);
    
    Task RemoveRolesAsync(string id, int tenantId);

    Task SetRolesAsync(string id, int tenantId, IEnumerable<string> roles);
}