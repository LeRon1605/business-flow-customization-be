using Application.Dtos;
using Application.Dtos.Identity;
using BuildingBlocks.Application.Identity.Dtos;
using BuildingBlocks.Kernel.Services;

namespace Identity.Application.Services.Interfaces;

public interface ITenantService : IScopedService
{
    Task<IdentityResult<TenantDto>> CreateAsync(string name, string avatarUrl);
    
    Task UpdateAsync(int tenantId, string name, string avatarUrl);
    
    Task DeleteAsync(int tenantId);
    
    Task AddUserToTenantAsync(string userId, bool isOwner, int tenantId);

    Task UpdateUserInTenantAsync(string userId, int tenantId, string fullname, string avatarUrl);
    
    Task RemoveUserFromTenantAsync(string userId, int tenantId);
}