using System.Reflection;
using BuildingBlocks.Infrastructure.Cdc.Attributes;
using BuildingBlocks.Infrastructure.Cdc.Models;

namespace BuildingBlocks.Infrastructure.Cdc.Extensions;

public static class CaptureTableModelExtensions
{
    public static List<Type> GetCdcTables()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly == null)
            throw new InvalidOperationException("Entry assembly is null");
        
        var referencedAssemblies = entryAssembly.GetReferencedAssemblies();
        
        var trackingTableModelTypes = new List<Type>();
        
        foreach (var assemblyName in referencedAssemblies)
        {
            var loadedAssembly = Assembly.Load(assemblyName);
            
            var assemblyTrackingTableModelTypes = loadedAssembly.GetTypes()
                .Where(t => typeof(ICaptureTableModel).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();
            
            trackingTableModelTypes.AddRange(assemblyTrackingTableModelTypes);
        }
        
        return trackingTableModelTypes;
    } 
}