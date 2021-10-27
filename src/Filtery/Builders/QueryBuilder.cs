using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Filtery.Extensions;
using Filtery.Models;
using Filtery.Models.Filter;
using Filtery.Models.Order;

namespace Filtery.Builders
{
    internal class QueryBuilder
    {
        internal IEnumerable<T> Build<T>(IEnumerable<T> list, FilteryRequest filteryRequest, Dictionary<string, Expression<Func<T, object>>> mappings)
        {
            var mainAndPredicate = PredicateBuilder.True<T>();
            var mainOrPredicate = PredicateBuilder.True<T>();

            foreach (var filterItem in filteryRequest.AndFilters)
            {
                var propertyName = GetPropertyName(filterItem, mappings);
                var operatorComparer = GetOperatorComparer(filterItem.Operation);

                var predicate = ExpressionBuilder.BuildPredicate<T>(filterItem.Value, operatorComparer, filterItem.CaseSensitive,propertyName);
                mainAndPredicate = mainAndPredicate.And(predicate);
            }

            foreach (var filterItem in filteryRequest.OrFilters)
            {
                var propertyName = GetPropertyName(filterItem, mappings);
                var operatorComparer = GetOperatorComparer(filterItem.Operation);

                var predicate = ExpressionBuilder.BuildPredicate<T>(filterItem.Value, operatorComparer, filterItem.CaseSensitive ,propertyName);
                mainOrPredicate = mainAndPredicate.Or(predicate);
            }

            var mainPredicate = PredicateBuilder.True<T>();
            mainPredicate = mainPredicate.And(mainAndPredicate);
            mainPredicate = mainPredicate.And(mainOrPredicate);

            var finalExpression = mainPredicate.Compile();

            list = list.Where(finalExpression);

            foreach (var orderOperation in filteryRequest.OrderOperations)
            {
                var propertyMapping = GetPropertyMapping(orderOperation.Key, mappings);
                
                if (orderOperation.Value == OrderOperation.Ascending)
                {
                    list = list.OrderBy(propertyMapping.Compile());
                }
                else
                {
                    list = list.OrderByDescending(propertyMapping.Compile());
                }
            }

            list = list.GetPage(filteryRequest.PageNumber, filteryRequest.PageSize);
            return list;
        }
        
        private string GetPropertyName<T>(FilterItem filterItem, Dictionary<string, Expression<Func<T, object>>> mappings)
        {
            return ((MemberExpression) mappings[filterItem.TargetFieldName.ToLower()].Body).Member.Name;
        }
        private Expression<Func<T, object>> GetPropertyMapping<T>(string filterName, Dictionary<string, Expression<Func<T, object>>> mappings)
        {
            return mappings[filterName.ToLower()];
        }
        private OperatorComparer GetOperatorComparer(FilterOperation filterOperation)
        {
            OperatorComparer response;
            switch (filterOperation)
            {
                case FilterOperation.Equal:
                    response = OperatorComparer.Equals;
                    break;
                case FilterOperation.NotEqual:
                    response = OperatorComparer.NotEqual;
                    break;
                case FilterOperation.Contains:
                    response = OperatorComparer.Contains;
                    break;
                case FilterOperation.GreaterThan:
                    response = OperatorComparer.GreaterThan;
                    break;
                case FilterOperation.LessThan:
                    response = OperatorComparer.LessThan;
                    break;
                case FilterOperation.GreaterThanOrEqual:
                    response = OperatorComparer.GreaterThanOrEqual;
                    break;
                case FilterOperation.LessThanOrEqual:
                    response = OperatorComparer.LessThanOrEqual;
                    break;
                case FilterOperation.StartsWith:
                    response = OperatorComparer.StartsWith;
                    break;
                case FilterOperation.EndsWith:
                    response = OperatorComparer.EndsWith;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterOperation), filterOperation, null);
            }

            return response;
        }
        
    }
}