using Application;
using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure.Cloudinary;
using BuildingBlocks.Infrastructure.Mail;
using BuildingBlocks.Presentation;
using Hub.Application;
using Hub.IntegrationEventHandlers;
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

builder.Services
    .AddEmailSender(builder.Configuration)
    .AddCloudinary(builder.Configuration);

var app = builder.Build();

app.RegisterCommonPipelines();

app.Run();