using Application;
using BuildingBlocks.Application;
using BuildingBlocks.Presentation;
using BusinessFlow.Application;
using BusinessFlow.Infrastructure.EfCore;
using BusinessFlow.IntegrationEventHandler;
using Presentation;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddSharedConfiguration();

builder
    .AddCommonServices()
    .AddApplicationAuth()
    .AddSharedServices()
    .AddEfCore<BusinessFlowDbContext>()
    .AddAssemblyMarker(typeof(ApplicationAssemblyMarker)
        , typeof(BuildingBlockApplicationAssemblyMarker)
        , typeof(SharedApplicationAssemblyMarker)
        , typeof(IntegrationEventAssemblyMarker)
        , typeof(SharedPresentationAssemblyMarker));

var app = builder.Build();

app.RegisterCommonPipelines();

await app.ApplyMigrationAsync<BusinessFlowDbContext>();

app.Run();