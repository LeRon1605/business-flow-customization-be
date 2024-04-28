using System.Reflection;

namespace BuildingBlocks.Shared.Helpers;

public static class AssemblyHelper
{
    private static readonly Dictionary<string, string> ServiceNames = new ()
    {
        { "Identity.Api", "Identity" },
        { "Hub.Api", "Hub" },
        { "BusinessFlow.Api", "BusinessFlow" }
    };
    
    public static Assembly[] GetReferencedAssembliesByType(params Type[] type)
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly == null)
            throw new InvalidOperationException("Entry assembly is null");

        var assemblies = new List<Assembly>();
        var referencedAssemblies = entryAssembly.GetReferencedAssemblies();
        
        foreach (var assemblyName in referencedAssemblies)
        {
            var loadedAssembly = Assembly.Load(assemblyName);
            var isAssemblyHasType = loadedAssembly.GetTypes().Any(t => type.Any(x => x.IsAssignableFrom(t)));
            
            if (isAssemblyHasType 
                && assemblies.All(x => x.FullName != loadedAssembly.FullName) 
                && loadedAssembly.FullName != entryAssembly.FullName)
            {
                assemblies.Add(loadedAssembly);
            }
        }

        return assemblies.ToArray();
    }

    public static string GetServiceName()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly == null)
            throw new InvalidOperationException("Entry assembly is null");

        return ServiceNames.GetValueOrDefault(entryAssembly.GetName().Name!) ?? string.Empty;
    }
}