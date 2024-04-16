using Microsoft.Extensions.Configuration;

namespace BuildingBlocks.Shared.Extensions;

public static class ConfigurationExtensions
{
    public static string GetRequiredValue(this IConfiguration configuration, string key)
    {
        var value = configuration[key];

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Configuration value for key '{key}' is required.");
        }

        return value;
    }

    public static string GetRequiredValue(this IConfigurationSection configuration, string key)
    {
        var value = configuration[key];

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Configuration value for key '{key}' is required.");
        }

        return value;
    }

    public static T GetRequiredValue<T>(this IConfiguration configuration, string key)
    {
        var value = configuration[key];

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Configuration value for key '{key}' is required.");
        }

        var convertedValue = Convert.ChangeType(value, typeof(T));
        if (convertedValue == null)
        {
            throw new InvalidOperationException($"Configuration value for key '{key}' is not in the right format.");
        }
        
        return (T)convertedValue;
    }
}