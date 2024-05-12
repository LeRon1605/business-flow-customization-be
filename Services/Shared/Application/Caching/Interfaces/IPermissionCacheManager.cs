using Application.Dtos;
using Application.Dtos.Submissions.Identity;
using BuildingBlocks.Application.Caching.Interfaces;

namespace Application.Caching.Interfaces;

public interface IPermissionCacheManager : ICachingManager
{
    Task<IEnumerable<PermissionDto>?> GetForRoleAsync(string role);
    
    Task SetForRoleAsync(string role, IEnumerable<PermissionDto> permissions);
    
    Task RemoveForRoleAsync(string role);
}