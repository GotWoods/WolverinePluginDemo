using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TemplatePOC.Core.Plugin;

public static class PluginLoader
{
    public static List<Assembly> RegisterTypesFromAssemblies<TInterface>(this IServiceCollection services, string folderPath, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var assemblyFiles = Directory.GetFiles(folderPath, "*.dll");
        var matchingAssemblies = new List<Assembly>();

        foreach (var assemblyFile in assemblyFiles)
            try
            {
                var assembly = Assembly.LoadFrom(assemblyFile);

                var typesToRegister = assembly.GetTypes()
                    .Where(t => typeof(TInterface).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                    .ToList();

                if (typesToRegister.Count > 0)
                {
                    matchingAssemblies.Add(assembly);

                    foreach (var type in typesToRegister)
                        switch (lifetime)
                        {
                            case ServiceLifetime.Transient:
                                services.AddTransient(typeof(TInterface), type);
                                break;
                            case ServiceLifetime.Scoped:
                                services.AddScoped(typeof(TInterface), type);
                                break;
                            case ServiceLifetime.Singleton:
                                services.AddSingleton(typeof(TInterface), type);
                                break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load or process assembly: {assemblyFile}. Reason: {ex.Message}");
            }

        return matchingAssemblies;
    }
}