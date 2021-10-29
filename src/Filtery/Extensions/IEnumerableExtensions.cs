using System.Collections.Generic;
using System.Linq;
using Filtery.Builders;
using Filtery.Configuration.Filtery;
using Filtery.Models;
using Filtery.Validators;
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantTypeArgumentsOfMethod

namespace Filtery.Extensions
{
    public static class IEnumerableExtensions
    { 
        public static IEnumerable<T> BuildFiltery<T>(this IList<T> list, AbstractFilteryMapping<T> mappingConfiguration, FilteryRequest filteryRequest)
        {
            var mappings= new ValidateFilterRequest().Validate(filteryRequest, mappingConfiguration);
            var query = new QueryBuilder().Build<T>(list, filteryRequest, mappings);
            
            return query;
        }
         
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> list, int pageNumber, int pageSize)
        {
            pageNumber -= 1;
            return list.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }
    }
}