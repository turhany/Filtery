using System.Collections.Generic;
using System.Linq;
using Filtery.Builders;
using Filtery.Configuration.Filtery;
using Filtery.Models;
using Filtery.Validators;

namespace Filtery.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> BuildFiltery<T>(this IQueryable<T> list, AbstractFilteryMapping<T> mappingConfiguration, FilteryRequest filteryRequest)
        {
            var mappings= new ValidateFilterRequest().Validate(filteryRequest, mappingConfiguration);
            var query = new QueryBuilder().Build<T>(list, filteryRequest, mappings);
            
            return query;
        }
         
        internal static IQueryable<T> GetPage<T>(this IQueryable<T> list, int pageNumber, int pageSize)
        {
            pageNumber -= 1;
            
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }
            
            return list.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
    }
}