using BuildingBlocks.Application.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Authorization;

namespace Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICurrentUser, MicroserviceCurrentUser>();
        
        return builder;
    }
}