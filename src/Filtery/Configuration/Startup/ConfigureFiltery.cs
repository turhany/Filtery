using System.Linq;
using Filtery.Configuration.Filtery;
using Microsoft.Extensions.DependencyInjection;

namespace Filtery.Configuration.Startup
{
    public static class ConfigureFiltery
    {
        public static IServiceCollection AddFilteryConfiguration(this IServiceCollection services, FilteryConfiguration configuration)
        {   
            var intefacesToRegister = configuration.RegisterMappingsFromAssemblyContaining.GetType().Assembly
                .GetTypes()
                .Where(t =>
                    t != typeof(IFilteryMapping<>) &&
                    t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IFilteryMapping<>)))
                .ToList();
            
            var typesToRegister = configuration.RegisterMappingsFromAssemblyContaining.GetType().Assembly
                .GetExportedTypes()
                .Where(t =>
                    t != typeof(IFilteryMapping<>) &&
                    t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition().UnderlyingSystemType == typeof(IFilteryMapping<>)))
                .ToList();
            
            foreach (var type in typesToRegister)
            {
                
            }
            
            return services;
        }
    }
}