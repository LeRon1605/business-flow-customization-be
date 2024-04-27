using System.Security.Claims;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Exceptions.Resources;
using Domain.Claims;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Presentation.Authorization;

public abstract class CurrentUser : ICurrentUser
{
    private string? _id;
    
    public string Id
    {
        get => _id ?? throw new ResourceUnauthorizedAccessException("User is not authenticated");
        set => _id = value;
    }

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

    protected CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(AppClaim.UserId);
    }

    public async Task<bool> IsInRoleAsync(string role)
    {
        var roles = await GetRolesAsync();
        return roles.Contains(role);    
    }

    public void SetId(string id)
    {
        Id = id;
    }

    public string GetClaim(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(claimType);
    }

    public abstract Task<List<string>> GetRolesAsync();
    public abstract Task<List<string>> GetPermissionsAsync();
}