using System.Reflection;
using BuildingBlocks.Kernel.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace BuildingBlocks.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoScanDependencyInjection(this IServiceCollection services, Assembly? assembly)
    {
        if (assembly == null)
            return services;
        
        services.Scan(scan =>
        {
            scan.FromAssemblyDependencies(assembly)
                .AddClasses(classes => classes.AssignableTo<ITransientService>())
                .AsMatchingInterface()
                .WithTransientLifetime();

            scan.FromAssemblyDependencies(assembly)
                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                .AsMatchingInterface()
                .WithScopedLifetime();
            
            scan.FromAssemblyDependencies(assembly)
                .AddClasses(classes => classes.AssignableTo<ISingletonService>())
                .AsMatchingInterface()
                .WithSingletonLifetime();
        });

        return services;
    }
    
    public static IServiceCollection AddOptionSetting<T>(this IServiceCollection services, IConfiguration configuration, string sectionName) where T : class
    {
        var obj = Activator.CreateInstance<T>();
        configuration.GetSection(sectionName).Bind(obj);

        services.AddSingleton(obj);

        return services;
    }

    public static IServiceCollection AddImplementationAsMatchingInterface<T>(this IServiceCollection services
        , Assembly? assembly
        , ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        if (assembly == null)
            return services;
        
        var types = assembly.GetTypes()
            .Where(x => !x.IsInterface
                        && !x.IsAbstract
                        && typeof(T).IsAssignableFrom(x));
        foreach (var matchType in types)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Scoped:
                    services.AddScoped(typeof(T), matchType);
                    break;
                
                case ServiceLifetime.Singleton:
                    services.AddSingleton(typeof(T), matchType);
                    break;
                
                case ServiceLifetime.Transient:
                    services.AddScoped(typeof(T), matchType);
                    break;
            }
        }
        return services;
    }
}