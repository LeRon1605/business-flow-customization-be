using Application.Caching.Interfaces;
using BuildingBlocks.Application.Caching;
using BuildingBlocks.Application.Caching.Interfaces;
using BuildingBlocks.Application.Identity;

namespace Application.Caching;

public class UserCacheManager : IUserCacheManager
{
    private readonly ICachingService _cacheService;
    
    public UserCacheManager(ICachingService cacheService)
    {
        _cacheService = cacheService;
    }
    
    public Task<IEnumerable<string>?> GetRolesAsync(string id, int tenantId)
    {
        var key = KeyGenerator.Generate("UserRole", $"{id}{tenantId}");
        return _cacheService.GetRecordAsync<IEnumerable<string>?>(key);
    }

    public Task RemoveRolesAsync(string id, int tenantId)
    {
        var key = KeyGenerator.Generate("UserRole", $"{id}{tenantId}");
        return _cacheService.RemoveRecordAsync(key);
    }

    public async Task SetRolesAsync(string id, int tenantId, IEnumerable<string> roles)
    {
        var key = KeyGenerator.Generate("UserRole", $"{id}{tenantId}");
        await _cacheService.SetRecordAsync(key, roles, TimeSpan.FromMinutes(30));
    }
}