using BuildingBlocks.Application.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Presentation.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<PermissionAuthorizationHandler> _logger;

    public PermissionAuthorizationHandler(ICurrentUser currentUser
        , ILogger<PermissionAuthorizationHandler> logger)
    {
        _currentUser = currentUser;
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
    {
        if (!_currentUser.IsAuthenticated)
        {
            _logger.LogError("Authorized failed, user is not authenticated!");
            return;
        }

        var permissions = await _currentUser.GetPermissionsAsync();
        var hasPermission = permissions.Contains(requirement.Permission);

        if (hasPermission)
        {
            context.Succeed(requirement);
        }
        else
        {
            _logger.LogError("Authorized failed, user does not has permission '{Permission}'!", requirement.Permission);
        }
    }
}