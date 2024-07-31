using System.Reflection;

namespace BuildingBlocks.Infrastructure.Cdc.Attributes;

public class CdcTableCaptureNameAttribute : Attribute
{
    public string Name { get; set; }
    
    public CdcTableCaptureNameAttribute(string name)
    {
        Name = name;
    }
}

public static class CdcTableCaptureNameAttributeExtensions
{
    public static string GetTableCaptureName(this Type type)
    {
        var tableName = type.Name;
        var tableCaptureName = type.GetCustomAttribute<CdcTableCaptureNameAttribute>()?.Name;
        
        if (!string.IsNullOrEmpty(tableCaptureName))
        {
            tableName = tableCaptureName;
        }

        return tableName;
    }
}