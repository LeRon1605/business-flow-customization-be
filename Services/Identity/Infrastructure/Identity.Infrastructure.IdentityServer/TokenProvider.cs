using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuildingBlocks.Application;
using Identity.Application.Services.Dtos;
using Identity.Application.Services.Interfaces;
using Identity.Domain.UserAggregate.Entities;
using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.IdentityServer;

public class TokenProvider : ITokenProvider
{
    private readonly JwtSetting _jwtSetting;
    private readonly ITokenService _tokenService;
    private readonly IClaimGenerator _claimGenerator;
    private readonly IdentityServerOptions _options;

    public TokenProvider(JwtSetting jwtSetting
        , ITokenService tokenService
        , IClaimGenerator claimGenerator
        , IdentityServerOptions options)
    {
        _jwtSetting = jwtSetting;
        _tokenService = tokenService;
        _claimGenerator = claimGenerator;
        _options = options;
    }

    public async Task<AccessToken> GenerateAccessTokenAsync(ApplicationUser user, int? tenantId)
    {
        var claims = await _claimGenerator.GenerateAsync(user, tenantId);

        var client = Config.Clients().First(x => x.ClientId == nameof(InternalApis.Identity));
        
        var identityUser = new IdentityServerUser(user.Id)
        {
            AdditionalClaims = claims,
            DisplayName = user.UserName,
            AuthenticationTime = DateTime.UtcNow,
            IdentityProvider = IdentityServerConstants.LocalIdentityProvider
        };
        
        var request = new TokenCreationRequest()
        {
            Subject = identityUser.CreatePrincipal(),
            IncludeAllIdentityClaims = true,
            ValidatedResources = new ResourceValidationResult(new Resources(Config.IdentityResources()
                , Config.ApiResources()
                , Config.ApiScopes())),
            ValidatedRequest = new ValidatedRequest()
            {
                Subject = identityUser.CreatePrincipal(),
                Client = client,
                ValidatedResources = new ResourceValidationResult(new Resources(Config.IdentityResources()
                    , Config.ApiResources()
                    , Config.ApiScopes())),
                Options = _options,
                ClientId = client.ClientId,
                ClientClaims = identityUser.AdditionalClaims,
                AccessTokenLifetime = _jwtSetting.ExpiresInMinute * 60
            }
        };
        
        var accessToken = await _tokenService.CreateAccessTokenAsync(request);
        foreach (var claim in claims)
        {
            accessToken.Claims.Add(claim);
        }
        
        return new AccessToken()
        {
            Id = accessToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value,
            Value = await _tokenService.CreateSecurityTokenAsync(accessToken)
        };
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
    
    public bool ValidateToken(string token, ref string id, ref List<Claim> claims)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        
        try
        {
            var key = Encoding.UTF8.GetBytes(_jwtSetting.Key);
            var tokenValidationParam = GetTokenValidationParam();
            var tokenInVerification = jwtTokenHandler.ValidateToken(token, tokenValidationParam, out var validatedToken);

            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                if (!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    return false;
                }
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            id = jwtTokenHandler.ReadJwtToken(token).Payload.Jti;
            claims = jwtTokenHandler.ReadJwtToken(token).Claims.ToList();
        }
    }

    private TokenValidationParameters GetTokenValidationParam()
    {
        return new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
        
            ValidIssuer = _jwtSetting.Issuer,
            ValidAudience = _jwtSetting.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key)),
        };
    }
}