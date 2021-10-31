using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Filtery.Configuration.Filtery;
using Filtery.Constants;
using Filtery.Extensions;
using Filtery.Models;
using Filtery.Models.Order;

namespace Filtery.Builders
{
    internal class QueryBuilder
    {
        internal IEnumerable<TEntity> Build<TEntity>(IEnumerable<TEntity> list, FilteryRequest filteryRequest, Dictionary<string, FilteryMappingItem<TEntity>> mappings)
        {
            var mainAndPredicate = PredicateBuilder.True<TEntity>();
            var mainOrPredicate = PredicateBuilder.True<TEntity>();

            foreach (var filterItem in filteryRequest.AndFilters)
            {
                var mapping = mappings[filterItem.TargetFieldName.ToLower()];
                
                var whereQuery = mapping.FilteryMappings.First(p => p.FilterOperations.Contains(filterItem.Operation)).Expression.ToString();
                foreach (var marker in  FilteryQueryValueMarker.ParameterCompareList)
                {
                    if (whereQuery.Contains(marker))
                    {
                        whereQuery = whereQuery.Replace(marker, FilteryConstant.DefaultParameterName);
                    }
                }

                var splittedQuery = whereQuery.Split(FilteryConstant.DefaultParameterName);
                var values = new List<object>();
                for (var i = 0; i < splittedQuery.Length - 1; i++)
                {
                    splittedQuery[i] += $"{FilteryConstant.DefaultParameterNamePrefix}{i}";
                    values.Add(filterItem.Value);

                    if ((i+1) >= splittedQuery.Length-1)
                    {
                        break;
                    }
                }

                whereQuery = string.Join(string.Empty,splittedQuery);
                var modifiedWhere = DynamicExpressionParser.ParseLambda<TEntity, bool>(new ParsingConfig(), true, whereQuery,values.ToArray());
                    
                mainAndPredicate = mainAndPredicate.And(modifiedWhere);
            }

            foreach (var filterItem in filteryRequest.OrFilters)
            {
                var mapping = mappings[filterItem.TargetFieldName.ToLower()];
                
                var whereQuery = mapping.FilteryMappings.First(p => p.FilterOperations.Contains(filterItem.Operation)).Expression.ToString();
                foreach (var marker in  FilteryQueryValueMarker.ParameterCompareList)
                {
                    if (whereQuery.Contains(marker))
                    {
                        whereQuery = whereQuery.Replace(marker, FilteryConstant.DefaultParameterName);
                    }
                }

                var splittedQuery = whereQuery.Split(FilteryConstant.DefaultParameterName);
                var values = new List<object>();
                for (var i = 0; i < splittedQuery.Length - 1; i++)
                {
                    splittedQuery[i] += $"{FilteryConstant.DefaultParameterNamePrefix}{i}";
                    values.Add(filterItem.Value);

                    if ((i+1) >= splittedQuery.Length-1)
                    {
                        break;
                    }
                }

                whereQuery = string.Join(string.Empty,splittedQuery);
                var modifiedWhere = DynamicExpressionParser.ParseLambda<TEntity, bool>(new ParsingConfig(), true, whereQuery,values.ToArray());
                    
                mainOrPredicate = mainOrPredicate.And(modifiedWhere);
            }

            var mainPredicate = PredicateBuilder.True<TEntity>();
            mainPredicate = mainPredicate.And(mainAndPredicate);
            mainPredicate = mainPredicate.And(mainOrPredicate);

            var finalExpression = mainPredicate.Compile();

            list = list.Where(finalExpression);

            foreach (var orderOperation in filteryRequest.OrderOperations)
            {
                var propertyMapping = GetPropertyMapping(orderOperation.Key, mappings);

                if (orderOperation.Value == OrderOperation.Ascending)
                {
                    list = list.OrderBy(propertyMapping.OrderExpression.Compile());
                }
                else
                {
                    list = list.OrderByDescending(propertyMapping.OrderExpression.Compile());
                }
            }

            list = list.GetPage(filteryRequest.PageNumber, filteryRequest.PageSize);
            return list;
        }
        
        internal IQueryable<TEntity> Build<TEntity>(IQueryable<TEntity> list, FilteryRequest filteryRequest, Dictionary<string, FilteryMappingItem<TEntity>> mappings)
        {
            var mainAndPredicate = PredicateBuilder.True<TEntity>();
            var mainOrPredicate = PredicateBuilder.True<TEntity>();

            foreach (var filterItem in filteryRequest.AndFilters)
            {
                var mapping = mappings[filterItem.TargetFieldName.ToLower()];
                
                var whereQuery = mapping.FilteryMappings.First(p => p.FilterOperations.Contains(filterItem.Operation)).Expression.ToString();
                foreach (var marker in  FilteryQueryValueMarker.ParameterCompareList)
                {
                    if (whereQuery.Contains(marker))
                    {
                        whereQuery = whereQuery.Replace(marker, FilteryConstant.DefaultParameterName);
                    }
                }

                var splittedQuery = whereQuery.Split(FilteryConstant.DefaultParameterName);
                var values = new List<object>();
                for (var i = 0; i < splittedQuery.Length - 1; i++)
                {
                    splittedQuery[i] += $"{FilteryConstant.DefaultParameterNamePrefix}{i}";
                    values.Add(filterItem.Value);

                    if ((i+1) >= splittedQuery.Length-1)
                    {
                        break;
                    }
                }

                whereQuery = string.Join(string.Empty,splittedQuery);
                var modifiedWhere = DynamicExpressionParser.ParseLambda<TEntity, bool>(new ParsingConfig(), true, whereQuery,values.ToArray());
                    
                mainAndPredicate = mainAndPredicate.And(modifiedWhere);
            }

            foreach (var filterItem in filteryRequest.OrFilters)
            {
                var mapping = mappings[filterItem.TargetFieldName.ToLower()];
                
                var whereQuery = mapping.FilteryMappings.First(p => p.FilterOperations.Contains(filterItem.Operation)).Expression.ToString();
                foreach (var marker in  FilteryQueryValueMarker.ParameterCompareList)
                {
                    if (whereQuery.Contains(marker))
                    {
                        whereQuery = whereQuery.Replace(marker, FilteryConstant.DefaultParameterName);
                    }
                }

                var splittedQuery = whereQuery.Split(FilteryConstant.DefaultParameterName);
                var values = new List<object>();
                for (var i = 0; i < splittedQuery.Length - 1; i++)
                {
                    splittedQuery[i] += $"{FilteryConstant.DefaultParameterNamePrefix}{i}";
                    values.Add(filterItem.Value);

                    if ((i+1) >= splittedQuery.Length-1)
                    {
                        break;
                    }
                }

                whereQuery = string.Join(string.Empty,splittedQuery);
                var modifiedWhere = DynamicExpressionParser.ParseLambda<TEntity, bool>(new ParsingConfig(), true, whereQuery,values.ToArray());
                    
                mainOrPredicate = mainOrPredicate.And(modifiedWhere);
            }

            var mainPredicate = PredicateBuilder.True<TEntity>();
            mainPredicate = mainPredicate.And(mainAndPredicate);
            mainPredicate = mainPredicate.And(mainOrPredicate);

            var finalExpression = mainPredicate;

            list = list.Where(finalExpression);

            foreach (var orderOperation in filteryRequest.OrderOperations)
            {
                var propertyMapping = GetPropertyMapping(orderOperation.Key, mappings);

                if (orderOperation.Value == OrderOperation.Ascending)
                {
                    list = list.OrderBy(propertyMapping.OrderExpression);
                }
                else
                {
                    list = list.OrderByDescending(propertyMapping.OrderExpression);
                }
            }

            list = list.GetPage(filteryRequest.PageNumber, filteryRequest.PageSize);
            return list;
        }

        private FilteryMappingItem<TEntity> GetPropertyMapping<TEntity>(string filterName, Dictionary<string, FilteryMappingItem<TEntity>> mappings)
        {
            return mappings[filterName.ToLower()];
        }
    }
}