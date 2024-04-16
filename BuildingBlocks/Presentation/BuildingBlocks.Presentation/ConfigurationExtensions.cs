using Microsoft.Extensions.Configuration;

namespace BuildingBlocks.Presentation;

public static class ConfigurationExtensions
{
    public static void AddSharedConfiguration(this ConfigurationManager configuration)
    {
        var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower();
        ArgumentNullException.ThrowIfNull(envName);
        
        configuration
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile($"appsettings.shared.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.shared.{envName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true);
    }
}