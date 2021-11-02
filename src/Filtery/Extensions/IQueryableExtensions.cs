using System.Linq;
using Filtery.Builders;
using Filtery.Configuration.Filtery;
using Filtery.Models;
using Filtery.Validators;

namespace Filtery.Extensions
{
    public static class IQueryableExtensions
    {
        public static FilteryResponse<T> BuildFiltery<T>(this IQueryable<T> list, AbstractFilteryMapping<T> mappingConfiguration, FilteryRequest filteryRequest)
        {
            var mappings= new ValidateFilterRequest().Validate(filteryRequest, mappingConfiguration);
            var query = new QueryBuilder().Build<T>(list, filteryRequest, mappings, out int totalItemCount);
            
            return new FilteryResponse<T>
            {
                Data = query.ToList(),
                PageNumber = filteryRequest.PageNumber,
                PageSize = filteryRequest.PageSize,
                TotalItemCount = totalItemCount
            };
        }
         
        internal static IQueryable<T> GetPage<T>(this IQueryable<T> list, int pageNumber, int pageSize)
        {
            pageNumber -= 1;
            
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }
            
            if (pageSize < 0)
            {
                pageNumber = 0;
            }
            
            return list.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
    }
}