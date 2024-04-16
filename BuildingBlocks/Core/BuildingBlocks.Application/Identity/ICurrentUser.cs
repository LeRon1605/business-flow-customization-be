namespace BuildingBlocks.Application.Identity;

public interface ICurrentUser
{
    string Id { get; }
    string Name { get; }
    string Email { get; }
    int TenantId { get; }
    bool IsAuthenticated { get; }
    
    string? GetClaim(string claimType);
    Task<List<string>> GetRolesAsync();
    Task<List<string>> GetPermissionsAsync();
    Task<bool> IsInRoleAsync(string role);
}