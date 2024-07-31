using Application;
using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure.Cdc;
using BuildingBlocks.Presentation;
using BusinessFlow.Application;
using BusinessFlow.Domain;
using BusinessFlow.Infrastructure.Cdc;
using BusinessFlow.Infrastructure.EfCore;
using BusinessFlow.IntegrationEventHandler;
using Presentation;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddSharedConfiguration();

builder
    .AddCommonServices()
    .AddSharedServices()
    .AddApplicationAuth()
    .AddEfCore<BusinessFlowDbContext>()
    .AddAssemblyMarker(typeof(ApplicationAssemblyMarker)
        , typeof(BuildingBlockApplicationAssemblyMarker)
        , typeof(SharedApplicationAssemblyMarker)
        , typeof(IntegrationEventAssemblyMarker)
        , typeof(SharedPresentationAssemblyMarker)
        , typeof(DomainAssemblyMarker)
        , typeof(CdcInfrastructureAssemblyMarker));

builder.Services
    .AddCdcService(builder.Configuration);

var app = builder.Build();

app.RegisterCommonPipelines();

await app.ApplyMigrationAsync<BusinessFlowDbContext>();

app.Run();