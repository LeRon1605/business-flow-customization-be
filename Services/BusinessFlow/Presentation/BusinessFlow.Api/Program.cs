using Application;
using BuildingBlocks.Application;
using BuildingBlocks.Presentation;
using BusinessFlow.Application;
using BusinessFlow.IntegrationEventHandler;
using Presentation;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddSharedConfiguration();

builder
    .AddCommonServices()
    .AddApplicationAuth()
    .AddServices()
    .AddAssemblyMarker(typeof(ApplicationAssemblyMarker)
        , typeof(BuildingBlockApplicationAssemblyMarker)
        , typeof(SharedApplicationAssemblyMarker)
        , typeof(IntegrationEventAssemblyMarker)
        , typeof(SharedPresentationAssemblyMarker));

var app = builder.Build();

app.RegisterCommonPipelines();

app.Run();