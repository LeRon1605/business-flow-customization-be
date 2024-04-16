using Application;
using BuildingBlocks.Presentation;
using Identity.Api.Extensions;
using Identity.Application;
using Identity.Infrastructure.EfCore;
using Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddSharedConfiguration();

builder
    .AddCommonServices()
    .AddIdentity()
    .AddApplicationIdentityServer()
    .AddSettings()
    .AddEfCore<AppIdentityDbContext>()
    .AddServices()
    .AddAssemblyMarker(typeof(SharedPresentationAssemblyMarker)
        , typeof(ApplicationAssemblyMarker)
        , typeof(SharedApplicationAssemblyMarker));

var app = builder.Build();

app.RegisterCommonPipelines();

app.UseIdentityServer();

await app.ApplyMigrationAsync<AppIdentityDbContext>();

app.Run();