using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using BuildingBlocks.Infrastructure.EfCore.Services;
using BuildingBlocks.Infrastructure.EfCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.EfCore;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddEfCore<TDbContext>(this IServiceCollection services, IConfiguration configuration) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(options =>
        {
            options.EnableSensitiveDataLogging();
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddScoped<IDbContextSaveChangesBehaviour, DbContextSaveChangesBehaviour>();
        services.AddScoped<IEntityStateDetector, EntityStateDetector>();
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
        
        services.AddScoped<DbContextFactory>(provider =>
        {
            var dbContext = provider.GetRequiredService<TDbContext>();
            return new DbContextFactory(dbContext);
        });
        
        services.AddScoped(typeof(IRepository<,>), typeof(EfCoreRepository<,>));
        services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(EfCoreReadOnlyRepository<,>));
        services.AddScoped(typeof(IBasicReadOnlyRepository<,>), typeof(EfCoreBasicReadOnlyRepository<,>));
        services.AddScoped(typeof(ISpecificationRepository<,>), typeof(EfCoreSpecificationRepository<,>));
        
        return services;
    }
}