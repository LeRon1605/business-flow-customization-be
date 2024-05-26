namespace BuildingBlocks.Shared.Extensions;

public static class DynamicExtension
{
    public static Dictionary<string, string> ToDictionary(this object obj)
    {
        var dictionary = new Dictionary<string, string>();
        
        var properties = obj.GetType().GetProperties();
        foreach (var property in properties)
        {
            var name = property.Name;
            var value = obj.GetType().GetProperty(property.Name)?.GetValue(obj, null);
            
            dictionary.Add(name, value?.ToString() ?? string.Empty);
        }
        
        return dictionary;
    }
}