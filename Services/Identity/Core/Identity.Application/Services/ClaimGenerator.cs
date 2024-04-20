using System.Security.Claims;
using BuildingBlocks.Application.Identity;
using Domain;
using Domain.Claims;
using Identity.Application.Services.Interfaces;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;

namespace Identity.Application.Services;

public class ClaimGenerator : IClaimGenerator
{
    public ClaimGenerator()
    {
    }
    
    public async Task<List<Claim>> GenerateAsync(ApplicationUser user, int? tenantId = null)
    {
        var claims = new List<Claim>()
        {
            new (AppClaim.UserId, user.Id),
            new (AppClaim.UserName, user.UserName!),
            new (AppClaim.Email, user.Email!)
        };

        if (tenantId != null)
        {
            if (user.Tenants.All(x => x.TenantId != tenantId.Value))
            {
                throw new UserNotInTenantException(user.Id, tenantId.Value);
            }
            
            claims.Add(new Claim(AppClaim.TenantId, tenantId.Value.ToString()));
        }
        else
        {
            var tenant = user.Tenants.FirstOrDefault();
            if (tenant != null)
            {
                claims.Add(new Claim(AppClaim.TenantId, tenant.TenantId.ToString()));
            }
        }

        return claims.ToList();
    }
}