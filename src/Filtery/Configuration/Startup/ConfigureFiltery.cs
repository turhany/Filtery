using System;
using System.Linq;
using Filtery.Configuration.Filtery;
using Microsoft.Extensions.DependencyInjection;

namespace Filtery.Configuration.Startup
{
    public static class ConfigureFiltery
    {
        private static readonly Type CompareType = typeof(IFilteryMapping<>);
        public static IServiceCollection AddFilteryConfiguration(this IServiceCollection services, FilteryConfiguration configuration)
        {
            foreach (var type in configuration.RegisterMappingsFromAssembly.GetTypes())
            {
                if (type.GetInterfaces().Any(p => p.IsGenericType && p.GetGenericTypeDefinition() == CompareType))
                {
                    var registerType = type
                        .GetInterfaces()
                        .First(p => p.Name.Equals(CompareType.Name, StringComparison.InvariantCultureIgnoreCase));
                    
                    services.Add(new ServiceDescriptor(registerType, type, ServiceLifetime.Scoped));
                }
            }
            
            return services;
        }
    }
}