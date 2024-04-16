using BuildingBlocks.Application;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Shared.Extensions;
using Identity.Api.Authorization;
using Identity.Application.Services.Dtos;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.UserAggregate.Entities;
using Identity.Infrastructure.EfCore;
using Identity.Infrastructure.IdentityServer;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;

            // Signin settings.
            options.SignIn.RequireConfirmedEmail = false;
        });

        return builder;
    }
    
    public static WebApplicationBuilder AddApplicationIdentityServer(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityServer()
            .AddAspNetIdentity<ApplicationUser>()
            .AddInMemoryClients(Config.Clients())
            .AddInMemoryIdentityResources(Config.IdentityResources())
            .AddInMemoryApiResources(Config.ApiResources())
            .AddInMemoryApiScopes(Config.ApiScopes())
            .AddDeveloperSigningCredential();
        
        var serviceProvider = builder.Services.BuildServiceProvider();
        var signingCredentialStore = serviceProvider.GetRequiredService<ISigningCredentialStore>();
        var signingKey = signingCredentialStore.GetSigningCredentialsAsync().Result;
        
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
        
                    ValidIssuer = InternalApis.Identity,
                    ValidAudience = nameof(InternalApis.Identity),
                    IssuerSigningKey = signingKey.Key,
                };
            });
        
        return builder;
    }
    
    public static WebApplicationBuilder AddSettings(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptionSetting<JwtSetting>(builder.Configuration, nameof(JwtSetting));
        builder.Services.AddOptionSetting<ForgetPasswordSetting>(builder.Configuration, nameof(ForgetPasswordSetting));
        
        return builder;
    }
    
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICurrentUser, IdentityCurrentUser>();
        
        return builder;
    }
}