using Microsoft.AspNetCore.Authorization;

namespace BuildingBlocks.Presentation.Authorization;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public string Permission { get; set; }
    
    public PermissionAuthorizationRequirement(string permission)
    {
        Permission = permission;
    }
}