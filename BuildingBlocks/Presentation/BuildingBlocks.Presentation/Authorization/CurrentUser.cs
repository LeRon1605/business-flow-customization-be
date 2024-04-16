using System.Security.Claims;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Exceptions.Resources;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Presentation.Authorization;

public abstract class CurrentUser : ICurrentUser
{
    public string Id 
        => _httpContextAccessor.HttpContext?.User?.FindFirstValue(AppClaim.UserId) 
           ?? throw new ResourceUnauthorizedAccessException("User is not authenticated");
    
    public string Name 
        => _httpContextAccessor.HttpContext?.User?.FindFirstValue(AppClaim.UserName)
            ?? throw new ResourceUnauthorizedAccessException("User is not authenticated");
    
    public string Email 
        => _httpContextAccessor.HttpContext?.User?.FindFirstValue(AppClaim.Email)
            ?? throw new ResourceUnauthorizedAccessException("User is not authenticated");
    
    public int TenantId
        => int.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(AppClaim.TenantId), out var tenantId) 
            ? tenantId 
            : throw new ResourceUnauthorizedAccessException("User is not authenticated");
    
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> IsInRoleAsync(string role)
    {
        var roles = await GetRolesAsync();
        return roles.Contains(role);    
    }
    
    public string GetClaim(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(claimType);
    }

    public abstract Task<List<string>> GetRolesAsync();
    public abstract Task<List<string>> GetPermissionsAsync();
}