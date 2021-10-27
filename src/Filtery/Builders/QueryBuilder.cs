using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Filtery.Models;

namespace Filtery.Builders
{
    internal class QueryBuilder
    {
        public IEnumerable<T> Build<T>(IEnumerable<T> list, FilteryRequest filteryRequest, Dictionary<string, Expression<Func<T, object>>> mappings)
        {
            Expression outerExpression = null;
            var parameter = Expression.Parameter(typeof(T), "e");

            Expression innerExpression = null;
            foreach (var filterItem in filteryRequest.AndFilters)
            {
               

            }
            
            return list;
        }
    }
}