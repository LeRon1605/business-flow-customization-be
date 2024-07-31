using Application;
using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure.Cdc;
using BuildingBlocks.Presentation;
using Presentation;
using Presentation.Extensions;
using Submission.Api.Extensions;
using Submission.Application;
using Submission.Domain;
using Submission.Infrastructure.Cdc;
using Submission.Infrastructure.EfCore;
using Submission.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddSharedConfiguration();

builder
    .AddCommonServices()
    .AddSharedServices()
    .AddSettings()
    .AddApplicationAuth()
    .AddEfCore<SubmissionDbContext>()
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

await app.ApplyMigrationAsync<SubmissionDbContext>();

app.Run();