using ApiGateway.Clients;
using ApiGateway.Clients.Abstracts;
using ApiGateway.Services;
using ApiGateway.Services.Abstracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiGateway.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiGatewaySwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(config =>
        {
            config.DocumentFilter<HideInDocsFilter>();
        });
        
        services.AddSwaggerForOcelot(configuration, options =>
        {
            options.GenerateDocsDocsForGatewayItSelf(config =>
            {
                config.GatewayDocsTitle = "Bff";
                config.GatewayDocsOpenApiInfo = new()
                {
                    Title = "Bff",
                    Version = "v1",
                };

                config.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        });

        return services;
    }
    
    public static IServiceCollection AddClients(this IServiceCollection services)
    {
        services.AddScoped<ISubmissionClient, SubmissionClient>();
        services.AddScoped<IBusinessFlowClient, BusinessFlowClient>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IFormService, FormService>();

        return services;
    }
}

public class HideInDocsFilter : IDocumentFilter
{
    private static readonly string[] _ignoredPaths = {
        "/configuration",
        "/outputcache/{region}"
    };
    
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach(var ignorePath in _ignoredPaths)
        {
            swaggerDoc.Paths.Remove(ignorePath);
        }
    }
}