using BuildingBlocks.Kernel.Services;

namespace Application.Identity.Interfaces;

public interface IUserInfoProvider : IScopedService
{
    Task<List<string>> GetRolesAsync(string userId, int tenantId);

    Task<List<string>> GetPermissionsAsync(string userId, int tenantId);
}