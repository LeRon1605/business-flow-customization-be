using System.Security.Claims;
using BuildingBlocks.Kernel.Services;
using Identity.Application.Services.Dtos;
using Identity.Domain.UserAggregate.Entities;

namespace Identity.Application.Services.Interfaces;

public interface ITokenProvider : IScopedService
{
    Task<AccessToken> GenerateAccessTokenAsync(ApplicationUser user, int? tenantId = null);
    
    string GenerateRefreshToken();

    bool ValidateToken(string token, ref string id, ref List<Claim> claims);
}