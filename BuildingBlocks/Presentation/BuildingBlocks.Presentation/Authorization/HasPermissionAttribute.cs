using Microsoft.AspNetCore.Authorization;

namespace BuildingBlocks.Presentation.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission) : base(permission)
    {
    } 
}