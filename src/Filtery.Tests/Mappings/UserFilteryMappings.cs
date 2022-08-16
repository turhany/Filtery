using Filtery.Configuration.Filtery;
using Filtery.Constants;
using Filtery.Models.Filter;
using Filtery.Tests.Model;

namespace Filtery.Tests.Mappings
{
    public class UserFilteryMappings : AbstractFilteryMapping<User>
    {
        public UserFilteryMappings()
        {
            mapper
                .Name("name")
                .OrderProperty(p =>p.FirstName)
                .Filter(p => p.FirstName.ToLower().Equals(FilteryQueryValueMarker.FilterStringValue.ToLower()), FilterOperation.Equal)
                .Filter(p => !p.FirstName.ToLower().Equals(FilteryQueryValueMarker.FilterStringValue.ToLower()), FilterOperation.NotEqual)
                .Filter(p => p.FirstName.ToLower().Contains(FilteryQueryValueMarker.FilterStringValue.ToLower()), FilterOperation.Contains)
                .Filter(p => p.FirstName.ToLower().StartsWith(FilteryQueryValueMarker.FilterStringValue.ToLower()), FilterOperation.StartsWith)
                .Filter(p => p.FirstName.ToLower().EndsWith(FilteryQueryValueMarker.FilterStringValue.ToLower()), FilterOperation.EndsWith);

            mapper
                .Name("age")
                .OrderProperty(p => p.Age)
                .Filter(p => p.Age == FilteryQueryValueMarker.FilterIntValue, FilterOperation.Equal)
                .Filter(p => p.Age != FilteryQueryValueMarker.FilterIntValue, FilterOperation.NotEqual)
                .Filter(p => p.Age > FilteryQueryValueMarker.FilterIntValue, FilterOperation.GreaterThan)
                .Filter(p => p.Age < FilteryQueryValueMarker.FilterIntValue, FilterOperation.LessThan)
                .Filter(p => p.Age >= FilteryQueryValueMarker.FilterIntValue, FilterOperation.GreaterThanOrEqual)
                .Filter(p => p.Age <= FilteryQueryValueMarker.FilterIntValue, FilterOperation.LessThanOrEqual);
            
            mapper
                .Name("date")
                .OrderProperty(p => p.Age)
                .Filter(p => p.Birthdate == FilteryQueryValueMarker.FilterDateTimeValue, FilterOperation.Equal)
                .Filter(p => p.Birthdate != FilteryQueryValueMarker.FilterDateTimeValue, FilterOperation.NotEqual)
                .Filter(p => p.Birthdate > FilteryQueryValueMarker.FilterDateTimeValue, FilterOperation.GreaterThan)
                .Filter(p => p.Birthdate < FilteryQueryValueMarker.FilterDateTimeValue, FilterOperation.LessThan)
                .Filter(p => p.Birthdate >= FilteryQueryValueMarker.FilterDateTimeValue, FilterOperation.GreaterThanOrEqual)
                .Filter(p => p.Birthdate <= FilteryQueryValueMarker.FilterDateTimeValue, FilterOperation.LessThanOrEqual);

            mapper
                .Name("licence")
                .OrderProperty(p => p.HasDriverLicence)
                .Filter(p => p.HasDriverLicence == FilteryQueryValueMarker.FilterBooleanValue, FilterOperation.Equal)
                .Filter(p => p.HasDriverLicence != FilteryQueryValueMarker.FilterBooleanValue, FilterOperation.NotEqual);
            
            mapper
                .NameWithoutOrder("parentnames")
                .Filter(p => p.ParentNames.Contains(FilteryQueryValueMarker.FilterStringValue), FilterOperation.Contains);

            mapper
                .Name("id")
                .OrderProperty(p => p.Id)
                .Filter(p => p.Id == FilteryQueryValueMarker.FilterGuidValue, FilterOperation.Equal)
                .Filter(p => p.Id != FilteryQueryValueMarker.FilterGuidValue, FilterOperation.NotEqual);
        }
    }
}