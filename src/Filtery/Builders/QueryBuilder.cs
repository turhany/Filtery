using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Filtery.Configuration.Filtery;
using Filtery.Constants;
using Filtery.Extensions;
using Filtery.Models;
using Filtery.Models.Filter;
using Filtery.Models.Order;

// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
// ReSharper disable PossibleMultipleEnumeration

namespace Filtery.Builders
{
    internal class QueryBuilder
    {
        internal IEnumerable<TEntity> Build<TEntity>(IEnumerable<TEntity> list, FilteryRequest filteryRequest,
            Dictionary<string, FilteryMappingItem<TEntity>> mappings, out int totalItemCount)
        {
            totalItemCount = 0;

            var finalExpression = BuildMainFilterQueryExpression(filteryRequest, mappings).Compile();
            list = list.Where(finalExpression);

            AddOrderOperations(list, filteryRequest, mappings);

            totalItemCount = list.Count();

            list = list.GetPage(filteryRequest.PageNumber, filteryRequest.PageSize);
            return list;
        }

        internal IQueryable<TEntity> Build<TEntity>(IQueryable<TEntity> list, FilteryRequest filteryRequest,
            Dictionary<string, FilteryMappingItem<TEntity>> mappings, out int totalItemCount)
        {
            totalItemCount = 0;

            var finalExpression = BuildMainFilterQueryExpression(filteryRequest, mappings);
            list = list.Where(finalExpression);

            AddOrderOperations(list, filteryRequest, mappings);

            totalItemCount = list.Count();

            list = list.GetPage(filteryRequest.PageNumber, filteryRequest.PageSize);
            return list;
        }

        #region Private Methods

        private FilteryMappingItem<TEntity> GetPropertyMapping<TEntity>(string filterName,
            Dictionary<string, FilteryMappingItem<TEntity>> mappings)
        {
            return mappings[filterName.ToLower()];
        }

        private Expression<Func<TEntity, bool>> BuildMainFilterQueryExpression<TEntity>(FilteryRequest filteryRequest,
            Dictionary<string, FilteryMappingItem<TEntity>> mappings)
        {
            Expression<Func<TEntity, bool>> mainPredicate = null;
            Expression<Func<TEntity, bool>> mainAndPredicate = null;
            Expression<Func<TEntity, bool>> mainOrPredicate = null;

            foreach (var filterItem in filteryRequest.AndFilters)
            {
                var whereExpression = GenerateFilterQueryExpression(mappings, filterItem);

                if (mainAndPredicate == null)
                {
                    mainAndPredicate = PredicateBuilder.Create(whereExpression);
                }
                else
                {
                    mainAndPredicate = mainAndPredicate.And(whereExpression);
                }
            }

            foreach (var filterItem in filteryRequest.OrFilters)
            {
                var whereExpression = GenerateFilterQueryExpression(mappings, filterItem);

                if (mainOrPredicate == null)
                {
                    mainOrPredicate = PredicateBuilder.Create(whereExpression);
                }
                else
                {
                    mainOrPredicate = mainOrPredicate.Or(whereExpression);
                }
            }


            if (mainAndPredicate == null && mainOrPredicate == null)
            {
                mainPredicate = PredicateBuilder.True<TEntity>();
            }
            else if (mainAndPredicate != null && mainOrPredicate != null)
            {
                mainPredicate = PredicateBuilder.Create(mainAndPredicate);
                mainPredicate = mainPredicate.And(mainOrPredicate);
            }
            else if (mainAndPredicate != null)
            {
                mainPredicate = PredicateBuilder.Create(mainAndPredicate);
            }
            else
            {
                mainPredicate = PredicateBuilder.Create(mainOrPredicate);
            }

            return mainPredicate;
        }

        private Expression<Func<TEntity, bool>> GenerateFilterQueryExpression<TEntity>(
            Dictionary<string, FilteryMappingItem<TEntity>> mappings, FilterItem filterItem)
        {
            var mapping = mappings[filterItem.TargetFieldName.ToLower()];

            var whereQuery = mapping.FilteryMappings.First(p => p.FilterOperations.Contains(filterItem.Operation))
                .Expression.ToString();

            var isDateTimeMarker = false;
            foreach (var marker in FilteryQueryValueMarker.ParameterCompareList)
            {
                if (whereQuery.Contains(marker))
                {
                    if (marker.Contains(nameof(FilteryQueryValueMarker.FilterDateTimeValue)) || 
                        marker.Contains(nameof(FilteryQueryValueMarker.FilterNullableDateTimeValue)))
                    {
                        isDateTimeMarker = true;
                    }
                    
                    whereQuery = whereQuery.Replace(marker, FilteryConstant.DefaultParameterName);
                }
            }

            var splittedQuery = whereQuery.Split(FilteryConstant.DefaultParameterName);
            var values = new List<object>();
            for (var i = 0; i < splittedQuery.Length - 1; i++)
            {
                splittedQuery[i] += $"{FilteryConstant.DefaultParameterNamePrefix}{i}";

                if (isDateTimeMarker && filterItem.Value.GetType() != typeof(DateTime))
                {
                    filterItem.Value = DateTime.Parse(filterItem.Value.ToString());
                }
                
                values.Add(filterItem.Value);

                if ((i + 1) >= splittedQuery.Length - 1)
                {
                    break;
                }
            }

            whereQuery = string.Join(string.Empty, splittedQuery);
            var modifiedWhere =
                DynamicExpressionParser.ParseLambda<TEntity, bool>(new ParsingConfig(), true, whereQuery,
                    values.ToArray());

            return modifiedWhere;
        }

        private void AddOrderOperations<TEntity>(IEnumerable<TEntity> list, FilteryRequest filteryRequest,
            Dictionary<string, FilteryMappingItem<TEntity>> mappings)
        {
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
        }

        private void AddOrderOperations<TEntity>(IQueryable<TEntity> list, FilteryRequest filteryRequest,
            Dictionary<string, FilteryMappingItem<TEntity>> mappings)
        {
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
        }

        #endregion
    }
}