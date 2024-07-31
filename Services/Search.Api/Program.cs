using Application;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Infrastructure.ElasticSearch;
using BuildingBlocks.Presentation;
using Presentation;
using Presentation.Extensions;
using Search.Application;
using Search.Infrastructure.ElasticSearch;
using Search.Infrastructure.ElasticSearch.IndexMappers;
using Search.IntegrationEventHandlers;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddSharedConfiguration();

builder
    .AddCommonServices()
    .AddSharedServices()
    .AddApplicationAuth()
    .AddAssemblyMarker(typeof(BuildingBlockApplicationAssemblyMarker)
        , typeof(SharedApplicationAssemblyMarker)
        , typeof(SharedPresentationAssemblyMarker)
        , typeof(ApplicationAssemblyMarker)
        , typeof(IntegrationEventAssemblyMarker));

builder.Services
    .AddScoped<IDataSeeder, IndexDataSeeder>()
    .AddScoped<SpaceIndexMapper>()
    .AddScoped<FormSubmissionIndexMapper>();

builder.Services
    .AddElasticSearch(builder.Configuration);

var app = builder.Build();

app.RegisterCommonPipelines();

await app.SeedDataAsync();

app.Run();