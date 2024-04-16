using BuildingBlocks.Application.Identity.Dtos;
using BuildingBlocks.Kernel.Services;

namespace Identity.Application.Services.Interfaces;

public interface IIdentityService : IScopedService
{
    Task<IdentityUserDto?> GetByIdAsync(string userId); 
    
    Task<IdentityUserDto?> GetByUserNameAsync(string username);
    
    Task<IEnumerable<string>> GetPermissionsForUserAsync(string userId, int tenantId);

    Task<IEnumerable<string>> GetRolesAsync(string userId, int tenantId);

    Task<IdentityResult<IdentityUserDto>> CreateUserAsync(string fullname
        , string username
        , string avatarUrl
        , string email
        , string password
        , string? phone = null
        , Guid? externalId = null);

    Task<bool> HasPermissionAsync(string userId, int tenantId, string permission);
    
    Task<IdentityResult<bool>> UpdateAsync(string userId, string fullname, string email, string avatarUrl);
    
    Task<IdentityResult<bool>> UpdateUserNameAsync(string userId, string username);
    
    Task<IdentityResult<bool>> DeleteAsync(string userId);
    
    Task<IdentityResult<bool>> GrantToRoleAsync(string userId, string role, int tenantId);
    
    Task<IdentityResult<bool>> UnGrantFromRoleAsync(string userId, string role, int tenantId);
}