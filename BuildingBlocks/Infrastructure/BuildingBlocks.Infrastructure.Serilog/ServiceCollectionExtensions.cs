using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BuildingBlocks.Infrastructure.Serilog;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        // "Serilog": {
        //     "SeqServerUrl": "http://localhost:5341",
        // }
        
        var seqServerUrl = configuration.GetRequiredValue("Serilog:ServerUrl");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.WithProperty("ApplicationContext", AssemblyHelper.GetServiceName())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq(seqServerUrl)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        return services;
    }
}