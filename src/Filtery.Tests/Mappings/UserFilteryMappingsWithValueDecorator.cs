using Filtery.Configuration.Filtery;
using Filtery.Constants;
using Filtery.Models.Filter;
using Filtery.Tests.Model;

namespace Filtery.Tests.Mappings
{
    public class UserFilteryMappingsWithValueDecorator : AbstractFilteryMapping<User>
    {
        public UserFilteryMappingsWithValueDecorator()
        {
            mapper
                .Name("name")
                .OrderProperty(p => p.FirstName)
                .ValueDecorator(value => ((string)value).Trim())
                .Filter(p => p.FirstName.Equals(FilteryQueryValueMarker.FilterStringValue), FilterOperation.Equal)
                .Filter(p => p.FirstName.Contains(FilteryQueryValueMarker.FilterStringValue), FilterOperation.Contains);
        }
    }
}
