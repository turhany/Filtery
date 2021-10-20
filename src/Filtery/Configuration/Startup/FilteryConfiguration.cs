using System;
using Filtery.Configuration.Filtery;

namespace Filtery.Configuration.Startup
{
    public class FilteryConfiguration
    {
        public int DefaultPageSize { get; set; }

        public Type RegisterMappingsFromAssemblyContaining { get; set; }
        // public List<Type> RegisterMappingsFromAssemblyContaining<T>() {
        //     
        //     var typesToRegister = typeof(T).Assembly
        //         .GetTypes()
        //         .Where(t =>
        //             t != typeof(IFilteryMapping<>) &&
        //             t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IFilteryMapping<>)))
        //         .ToList();
        //
        //     // foreach (var type in typesToRegister)
        //     // {
        //     //    
        //     // }
        //
        //     return typesToRegister;
        // }
    }
}