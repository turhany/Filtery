using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Filtery.Configuration.Filtery;
using Filtery.Constants;
using Filtery.Exceptions;
using Filtery.Extensions;
using Filtery.Models;

namespace Filtery.Validators
{
    public class ValidateFilterRequest
    {
        public Dictionary<string, FilteryMappingItem<TEntity>> Validate<TEntity>(FilteryRequest filteryRequest, AbstractFilteryMapping<TEntity> mappingConfiguration)
        {
            if (mappingConfiguration == null)
            {
                throw new NullFilteryMappingException();
            }

            if (filteryRequest == null)
            {
                throw new NullFilterRequestException();
            }
            
            var mappings = mappingConfiguration
                .GetFieldValue<FilteryMapper<TEntity>>(FilteryConstant.MapperFiledName)
                .GetFieldValue<Dictionary<string, FilteryMappingItem<TEntity>>>(FilteryConstant.MappingListFieldName);
            
            foreach (var filterItem in filteryRequest.AndFilters)
            {
                if (!mappings.ContainsKey(filterItem.TargetFieldName.ToLowerInvariant()))
                {
                    throw new NotConfiguredFilterMappingException(filterItem.TargetFieldName.ToLowerInvariant());
                }
                if (!mappings[filterItem.TargetFieldName.ToLowerInvariant()].FilteryMappings.SelectMany(p => p.FilterOperations).Contains(filterItem.Operation))
                {
                    var message = $"'{filterItem.Operation.ToString()}' operation not supported for '{filterItem.TargetFieldName}'";
                    throw new NotSupportedFilterOperationForType(message);
                }
            }
            
            foreach (var filterItem in filteryRequest.AndFilters)
            {
                if (!mappings.ContainsKey(filterItem.TargetFieldName.ToLowerInvariant()))
                {
                    throw new NotConfiguredFilterMappingException(filterItem.TargetFieldName.ToLowerInvariant());
                }
                if (!mappings[filterItem.TargetFieldName.ToLowerInvariant()].FilteryMappings.SelectMany(p => p.FilterOperations).Contains(filterItem.Operation))
                {
                    var message = $"'{filterItem.Operation.ToString()}' operation not supported for '{filterItem.TargetFieldName}'";
                    throw new NotSupportedFilterOperationForType(message);
                }
            }
            
            foreach (var orderOperation in filteryRequest.OrderOperations)
            {
                if (!mappings.ContainsKey(orderOperation.Key.ToLowerInvariant()))
                {
                    throw new NotConfiguredOrderException(orderOperation.Key.ToLowerInvariant());
                }

                if (mappings[orderOperation.Key.ToLowerInvariant()].OrderExpression == null)
                {
                    throw new NotConfiguredFilterMappingException($"Order Expression not found for Key: \"{orderOperation.Key}\"");
                }
            }

            return mappings;
        }
    }
}