using System.Collections.Generic;
using Filtery.Builders;
using Filtery.Configuration.Filtery;
using Filtery.Models;
using Filtery.Validators;

namespace Filtery.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> BuildFiltery<T>(this IList<T> list, AbstractFilteryMapping<T> mappingConfiguration, FilteryRequest filteryRequest)
        {
            var mappings = new ValidateFilterRequest().Validate(filteryRequest, mappingConfiguration);
            var query = new QueryBuilder().Build<T>(list, filteryRequest, mappings);
            
            return query;
        }
    }
}