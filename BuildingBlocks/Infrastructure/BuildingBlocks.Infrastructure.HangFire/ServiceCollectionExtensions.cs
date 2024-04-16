using BuildingBlocks.Application.Schedulers;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BuildingBlocks.Infrastructure.HangFire;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHangFireBackGroundJob(this IServiceCollection services
        , IConfiguration configuration
        , string databaseName)
    {
        var mongoUrlBuilder = new MongoUrlBuilder(configuration.GetConnectionString("HangFireMongoDb"));
        var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
        
        services.AddHangfire(x => x
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseMongoStorage(mongoClient, databaseName, new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                    BackupStrategy = new CollectionMongoBackupStrategy()
                },
                CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection,
                Prefix = "HangFire",
                CheckConnection = false
            })
        );

        services.AddHangfireServer(serverOptions =>
        {
            serverOptions.ServerName = "Hangfire";
        });

        services.AddScoped<IBackGroundJobManager, HangFireBackGroundJobManager>();
        services.AddScoped<IBackGroundJobPublisher, BackGroundJobPublisher>();
        
        return services;
    }
}