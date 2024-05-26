using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Infrastructure.MongoDb.Repositories;
using BuildingBlocks.Infrastructure.MongoDb.Settings;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BuildingBlocks.Infrastructure.MongoDb;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        // "MongoDb": {
        //     "Host": "localhost",
        //     "Port": "27018",
        //     "UserName": "root",
        //     "Password": "pbl62023"
        // },
        
        var setting = configuration.GetSetting<MongoDbSetting>("MongoDb");
        
        services.AddSingleton(provider =>
        {
            var mongoClient = new MongoClient(setting.ConnectionString);
            return mongoClient.GetDatabase(AssemblyHelper.GetServiceName());
        });
        
        services.AddScoped(typeof(IRepository<,>), typeof(MongoDbRepository<,>));
        services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(MongoDbReadOnlyRepository<,>));
        services.AddScoped(typeof(IBasicReadOnlyRepository<,>), typeof(MongoDbBasicReadOnlyRepository<,>));
        services.AddScoped(typeof(ISpecificationRepository<,>), typeof(MongoDbSpecificationRepository<,>));
        
        return services;
    }
}