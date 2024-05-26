using Domain.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BuildingBlocks.Presentation.Authorization;

public class ApplicationAuthorizationPolicyProvider : IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider _defaultPolicyProvider;

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => _defaultPolicyProvider.GetFallbackPolicyAsync();

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => _defaultPolicyProvider.GetDefaultPolicyAsync();

    public ApplicationAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        _defaultPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(AppPermission.Default, StringComparison.OrdinalIgnoreCase))
        {
            var policyBuilder = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new PermissionAuthorizationRequirement(policyName));
            
            return Task.FromResult(policyBuilder.Build());
        }
        
        if (policyName.StartsWith("InternalApi", StringComparison.OrdinalIgnoreCase))
        {
            var policyBuilder = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new InternalApiAuthorizationRequirement());
            
            return Task.FromResult(policyBuilder.Build());
        }

        return _defaultPolicyProvider.GetPolicyAsync(policyName);
    }
}