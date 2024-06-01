using ApiGateway.Extensions;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation;
using Newtonsoft.Json;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddSharedConfiguration();

builder.Services
    .AddInternalApis(builder.Configuration)
    .AddClients()
    .AddApplicationServices()
    .AddHttpContextAccessor()
    .AddEndpointsApiExplorer()
    .AddApplicationCors()
    .AddApplicationSerilog(builder.Configuration)
    .AddOcelot();

builder.Services.AddControllers().AddNewtonsoftJson(config =>
{
    config.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
});

builder.AddAssemblyMarker(typeof(SharedPresentationAssemblyMarker));

builder.Services.AddApiGatewaySwagger(builder.Configuration);

builder.Host.UseSerilog();

var app = builder.Build();

app.UseExceptionHandling();

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