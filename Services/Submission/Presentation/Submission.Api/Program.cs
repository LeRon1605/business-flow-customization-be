using Application;
using BuildingBlocks.Application;
using BuildingBlocks.Presentation;
using Presentation;
using Presentation.Extensions;
using Submission.Application;
using Submission.Domain;
using Submission.Infrastructure.EfCore;
using Submission.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddSharedConfiguration();

builder
    .AddCommonServices()
    .AddSharedServices()
    .AddApplicationAuth()
    .AddEfCore<SubmissionDbContext>()
    .AddAssemblyMarker(typeof(ApplicationAssemblyMarker)
        , typeof(BuildingBlockApplicationAssemblyMarker)
        , typeof(SharedApplicationAssemblyMarker)
        , typeof(IntegrationEventAssemblyMarker)
        , typeof(SharedPresentationAssemblyMarker)
        , typeof(DomainAssemblyMarker));

var app = builder.Build();

app.RegisterCommonPipelines();

await app.ApplyMigrationAsync<SubmissionDbContext>();

app.Run();