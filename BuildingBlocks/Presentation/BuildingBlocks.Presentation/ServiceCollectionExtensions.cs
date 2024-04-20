using System.Reflection;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Mappers;
using BuildingBlocks.EventBus.RabbitMQ;
using BuildingBlocks.Infrastructure.Cloudinary;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.HangFire;
using BuildingBlocks.Infrastructure.Redis;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authorization;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BuildingBlocks.Presentation;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddCommonServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoScanDependencyInjection(Assembly.GetEntryAssembly());
        builder.Services.AddInternalApis(builder.Configuration);
        builder.AddApplicationServices();
        builder.AddInfrastructureServices();
        builder.AddPresentationServices();
        
        return builder;
    }
    
    public static WebApplicationBuilder AddPresentationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddNewtonsoftJson();
        builder.Services.AddSwagger();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddApplicationCors();
        
        return builder;
    }

    public static WebApplicationBuilder AddApplicationAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = InternalApis.Identity;
                options.Audience = AssemblyHelper.GetServiceName();
                options.RequireHttpsMetadata = false;
            });
        builder.Services.AddAuthorization();

        return builder;
    }

    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatr();
        builder.Services.AddApplicationMapper();
        builder.Services.AddDataSeeder();
        
        return builder;
    }
    
    public static WebApplicationBuilder AddInfrastructureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRabbitMq(builder.Configuration);
        builder.Services.AddRedisCache(builder.Configuration, "BusinessFlowExecutor");
        builder.Services.AddHangFireBackGroundJob(builder.Configuration, AssemblyHelper.GetServiceName());
        builder.Services.AddCloudinary(builder.Configuration);
        builder.Services.AddApplicationSerilog(builder.Configuration);
        builder.Host.UseSerilog();

        return builder;
    }

    public static WebApplicationBuilder AddEfCore<TDbContext>(this WebApplicationBuilder builder) where TDbContext : DbContext
    {
        builder.Services.AddEfCore<TDbContext>(builder.Configuration);
        return builder;
    }

    public static WebApplicationBuilder AddAssemblyMarker(this WebApplicationBuilder builder, params Type[] types)
    {
        return builder;
    }

    public static WebApplicationBuilder AddAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        builder.Services.AddSingleton<IAuthorizationPolicyProvider, ApplicationAuthorizationPolicyProvider>();
        
        return builder;
    }
}