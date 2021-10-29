using System;
using System.Collections.Generic;
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
        public (Dictionary<string, Expression<Func<T, object>>>, Dictionary<string, Expression<Func<T, bool>>>) Validate<T>(FilteryRequest filteryRequest, AbstractFilteryMapping<T> mappingConfiguration)
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
                .GetFieldValue<FilteryMapper<T>>(FilteryConstant.MapperFiledName)
                .GetFieldValue<Dictionary<string, Expression<Func<T, object>>>>(FilteryConstant.MappingListFieldName);
            
            var customMappings = mappingConfiguration
                .GetFieldValue<FilteryMapper<T>>(FilteryConstant.MapperFiledName)
                .GetFieldValue<Dictionary<string, Expression<Func<T, bool>>>>(FilteryConstant.CustomMappingListFieldName);
            
            foreach (var filterItem in filteryRequest.AndFilters)
            {
                if (!mappings.ContainsKey(filterItem.TargetFieldName.ToLowerInvariant()) &&
                    !customMappings.ContainsKey(filterItem.TargetFieldName.ToLowerInvariant()))
                {
                    throw new NotConfiguredFilterMappingException(filterItem.TargetFieldName.ToLowerInvariant());
                }
            }
            
            foreach (var filterItem in filteryRequest.AndFilters)
            {
                if (!mappings.ContainsKey(filterItem.TargetFieldName.ToLowerInvariant()) &&
                    !customMappings.ContainsKey(filterItem.TargetFieldName.ToLowerInvariant()))
                {
                    throw new NotConfiguredFilterMappingException(filterItem.TargetFieldName.ToLowerInvariant());
                }
            }
            
            foreach (var orderOperation in filteryRequest.OrderOperations)
            {
                if (!mappings.ContainsKey(orderOperation.Key.ToLowerInvariant()) &&
                    !customMappings.ContainsKey(orderOperation.Key.ToLowerInvariant()))
                {
                    throw new NotConfiguredOrderException(orderOperation.Key.ToLowerInvariant());
                }
            }

            return (mappings, customMappings);
        }
    }
}