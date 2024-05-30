using Application;
using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure.CloudflareR2;
using BuildingBlocks.Infrastructure.Mail;
using BuildingBlocks.Infrastructure.MongoDb;
using BuildingBlocks.Presentation;
using Hub.Application;
using Hub.Application.Clients;
using Hub.Domain;
using Hub.Infrastructure.MongoDb;
using Hub.Infrastructure.SignalR.Hubs;
using Hub.IntegrationEventHandlers;
using Presentation;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddSharedConfiguration();

builder
    .AddCommonServices()
    .AddApplicationAuth()
    .AddSharedServices()
    .AddAssemblyMarker(typeof(ApplicationAssemblyMarker)
        , typeof(BuildingBlockApplicationAssemblyMarker)
        , typeof(SharedApplicationAssemblyMarker)
        , typeof(IntegrationEventAssemblyMarker)
        , typeof(SharedPresentationAssemblyMarker)
        , typeof(DomainAssemblyMarker)
        , typeof(MongoDbInfrastructureAssemblyMarker));

builder.Services.AddMongoDb(builder.Configuration);

builder.Services.AddSignalR();

builder.Services
    .AddEmailSender(builder.Configuration)
    .AddCloudflareR2(builder.Configuration);

var app = builder.Build();

app.MapHub<NotificationHub>("/notification-hub");

app.RegisterCommonPipelines();


app.Run();