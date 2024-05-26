using BuildingBlocks.Application.Identity;
using Domain.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Presentation.Authorization;

public class InternalApiAuthorizationHandler : AuthorizationHandler<InternalApiAuthorizationRequirement>
{
    private readonly ICurrentUser _currentUser;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<PermissionAuthorizationHandler> _logger;

    public InternalApiAuthorizationHandler(ICurrentUser currentUser
        , IHttpContextAccessor httpContextAccessor
        , ILogger<PermissionAuthorizationHandler> logger)
    {
        _currentUser = currentUser;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, InternalApiAuthorizationRequirement requirement)
    {
        var scopeClaim = context.User.FindFirst("scope");
        if (scopeClaim is null || scopeClaim.Value != "microservice")
        {
            _logger.LogError("Authorized failed, user does not has valid scope!");
            return;
        }

        var httpContext = _httpContextAccessor.HttpContext;
        var userId = httpContext?.Request.Headers[AppClaim.MicroserviceUserId].ToString();
        var tenantId = httpContext?.Request.Headers[AppClaim.MicroserviceTenantId].ToString();
        
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(tenantId))
        {
            _logger.LogError("Authorized success without user claims");
            context.Succeed(requirement);
            return;
        }

        _currentUser.IsAuthenticated = true;
        _currentUser.Id = userId;
        _currentUser.TenantId = int.Parse(tenantId!);
        
        context.Succeed(requirement);
    }
}