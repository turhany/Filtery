using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
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
        private static readonly ValidateFilterRequest _validator = new ValidateFilterRequest();
        private static readonly QueryBuilder _queryBuilder = new QueryBuilder();

        public static IEnumerable<T> BuildFilteryQuery<T>(this IList<T> list, AbstractFilteryMapping<T> mappingConfiguration, FilteryRequest filteryRequest)
        {
            var mappings = _validator.Validate(filteryRequest, mappingConfiguration);
            var query = _queryBuilder.Build<T>(list, filteryRequest, mappings, out int totalItemCount);            

            return query;
        }

        public static FilteryResponse<T> BuildFiltery<T>(this IList<T> list, AbstractFilteryMapping<T> mappingConfiguration, FilteryRequest filteryRequest)
        {
            var mappings = _validator.Validate(filteryRequest, mappingConfiguration);
            var query = _queryBuilder.Build<T>(list, filteryRequest, mappings, out int totalItemCount);

            return new FilteryResponse<T>
            {
                Data = query.ToList(),
                PageNumber = filteryRequest.PageNumber,
                PageSize = filteryRequest.PageSize,
                TotalItemCount = totalItemCount
            };
        }

        public static async Task<FilteryResponse<T>> BuildFilteryAsync<T>(this IList<T> list, AbstractFilteryMapping<T> mappingConfiguration, FilteryRequest filteryRequest)
        {
            var mappings = _validator.Validate(filteryRequest, mappingConfiguration);
            var query = _queryBuilder.Build<T>(list, filteryRequest, mappings, out int totalItemCount);

            return new FilteryResponse<T>
            {
                Data = await query.ToDynamicListAsync<T>(),
                PageNumber = filteryRequest.PageNumber,
                PageSize = filteryRequest.PageSize,
                TotalItemCount = totalItemCount
            };
        }

        internal static IEnumerable<T> GetPage<T>(this IEnumerable<T> list, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (pageSize < 0)
            {
                pageNumber = 0;
            }

            pageNumber -= 1;

            return list.Skip(pageSize * pageNumber).Take(pageSize).ToList();
        }
    }
}