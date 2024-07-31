using System.Reflection;

namespace BuildingBlocks.Application.SearchEngines.Attributes;

public class IndexNameAttribute : Attribute
{
    public string Name { get; set; }
    
    public IndexNameAttribute(string name)
    {
        Name = name;
    }
}

public static class IndexNameAttributeExtensions
{
    public static string GetIndexName(this Type type)
    {
        var tableName = type.Name;
        var tableCaptureName = type.GetCustomAttribute<IndexNameAttribute>()?.Name;
        
        if (!string.IsNullOrEmpty(tableCaptureName))
        {
            tableName = tableCaptureName;
        }

        return tableName;
    }
}