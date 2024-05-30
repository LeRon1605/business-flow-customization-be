using Application.Caching;
using Application.Caching.Interfaces;
using Application.Clients;
using Application.Clients.Interfaces;
using Application.Identity;
using Application.Identity.Interfaces;
using BuildingBlocks.Application.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Authorization;

namespace Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddSharedServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICurrentUser, MicroserviceCurrentUser>();
        builder.Services.AddScoped<IUserInfoProvider, UserInfoProvider>();
        builder.Services.AddScoped<IIdentityClient, IdentityClient>();
        builder.Services.AddScoped<IUserCacheManager, UserCacheManager>();
        builder.Services.AddScoped<IPermissionCacheManager, PermissionCacheManager>();
        
        return builder;
    }
}