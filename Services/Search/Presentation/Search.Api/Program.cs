using Application;
using BuildingBlocks.Application;
using BuildingBlocks.Presentation;
using Presentation;
using Presentation.Extensions;
using Search.Api.Extensions;
using Search.Application;
using Search.IntegrationEventHandlers;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddSharedConfiguration();

builder
    .AddCommonServices()
    .AddSharedServices()
    .AddApplicationAuth()
    .AddElasticSearch()
    .AddAssemblyMarker(typeof(BuildingBlockApplicationAssemblyMarker)
        , typeof(SharedApplicationAssemblyMarker)
        , typeof(SharedPresentationAssemblyMarker)
        , typeof(ApplicationAssemblyMarker)
        , typeof(IntegrationEventAssemblyMarker));

var app = builder.Build();

app.RegisterCommonPipelines();

await app.SeedDataAsync();

app.Run();