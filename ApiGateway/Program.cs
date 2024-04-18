using ApiGateway.Extensions;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddSharedConfiguration();

builder.Services
    .AddEndpointsApiExplorer()
    .AddApplicationCors()
    .AddApplicationSerilog(builder.Configuration)
    .AddOcelot();

builder.AddAssemblyMarker(typeof(SharedPresentationAssemblyMarker));

builder.Services.AddApiGatewaySwagger(builder.Configuration);

builder.Host.UseSerilog();

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerForOcelotUI();   
}

app.UseRouting();

app.UseCors("BusinessFlow");

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#pragma warning restore ASP0014

app.UseOcelot();

app.Run();