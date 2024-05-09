using BuildingBlocks.Application;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Mappers;
using BuildingBlocks.Domain.Events;
using BuildingBlocks.Domain.Services;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BuildingBlocks.Presentation;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BusinessFlow",
                Version = "v1"
            });

            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                BearerFormat = "JWT",
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
        
        return services;
    }
    
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            var mediatrAssemblies = AssemblyHelper.GetReferencedAssembliesByType(typeof(ICqrsRequest));
            if (mediatrAssemblies.Any())
                config.RegisterServicesFromAssemblies(mediatrAssemblies);

            config.RegisterServicesFromAssembly(typeof(BuildingBlockApplicationAssemblyMarker).Assembly);
        });

        return services;
    }
    
    public static IServiceCollection AddApplicationCors(this IServiceCollection services)
    {
        services.AddCors(o => o.AddPolicy("BusinessFlow", builder =>
        {
            builder.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));

        return services;
    }
    
    public static IServiceCollection AddInternalApis(this IServiceCollection services, IConfiguration configuration)
    {
        var obj = Activator.CreateInstance<InternalApis>();
        configuration.GetSection(nameof(InternalApis)).Bind(obj);

        return services;
    }

    public static IServiceCollection AddDataSeeder(this IServiceCollection services)
    {
        var assemblies = AssemblyHelper.GetReferencedAssembliesByType(typeof(IDataSeeder));
        foreach (var assembly in assemblies)
        {
            services.AddImplementationAsMatchingInterface<IDataSeeder>(assembly);   
        }
        
        return services;
    }

    public static IServiceCollection AddApplicationMapper(this IServiceCollection services)
    {
        var assemblies = AssemblyHelper.GetReferencedAssembliesByType(typeof(MappingProfile));
        foreach (var assembly in assemblies)
        {
            services.AddAutoMapper(assembly);
        }
        
        return services;
    }
    
    public static IServiceCollection AddPersistedDomainEventHandlers(this IServiceCollection services)
    {
        var assemblies = AssemblyHelper.GetReferencedAssembliesByType(typeof(IDomainEvent));
        var handlerAssemblies = AssemblyHelper.GetReferencedAssembliesByType(typeof(IPersistedDomainEventHandler));
        
        var domainEventTypes = assemblies.SelectMany(x => x.GetTypes())
            .Where(x => typeof(IDomainEvent).IsAssignableFrom(x))
            .ToList();
        
        foreach (var domainEventType in domainEventTypes)
        {
            var handlerType = typeof(IPersistedDomainEventHandler<>).MakeGenericType(domainEventType);
            var handlers = handlerAssemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsInterface
                            && !x.IsAbstract 
                            && handlerType.IsAssignableFrom(x));

            foreach (var handler in handlers)
            {
                services.AddScoped(handlerType, handler);
            }
        }

        return services;
    }
}