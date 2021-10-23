using System.Reflection;

namespace Filtery.Configuration.Startup
{
    public class FilteryConfiguration
    {
        public int DefaultPageSize { get; set; }
        public bool CaseSensitive { get; set; }
        public Assembly RegisterMappingsFromAssembly { get; set; }
    }
}