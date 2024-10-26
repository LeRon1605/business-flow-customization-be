﻿using BuildingBlocks.Application;
using IdentityModel;
using IdentityServer4.Models;

namespace Identity.Infrastructure.IdentityServer;

public class Config
{
    public static IEnumerable<Client> Clients()
    {
        var internalApis = typeof(InternalApis).GetProperties()
            .Select(x => x.Name)
            .ToList();

        return internalApis.Select(x => new Client()
        {
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientId = x,
            ClientSecrets =
            {
                new Secret(x.Sha256())
            },
            AllowedScopes =
            {
                "internal-api",
                "microservice"
            }
        });
    }

    public static IEnumerable<ApiResource> ApiResources()
    {
        var internalApis = typeof(InternalApis).GetProperties()
            .Select(x => x.Name)
            .ToList();
        
        var apiResources = internalApis.Select(x => new ApiResource(x)
        {
            Scopes =
            {
                "internal-api",
                "microservice"
            }
        }).ToList();
        
        return apiResources;
    }
    
    public static IEnumerable<ApiScope> AuthScopes()
    {
        return new List<ApiScope>()
        {
            new ("internal-api"),
        };
    }

    public static IEnumerable<ApiScope> ApiScopes()
    {
        return new List<ApiScope>()
        {
            new ("internal-api"),
            new ("microservice")
            {
                UserClaims =
                {
                    JwtClaimTypes.Audience
                }
            }
        };
    }

    public static IEnumerable<IdentityResource> IdentityResources()
    {
        return new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    } 
}