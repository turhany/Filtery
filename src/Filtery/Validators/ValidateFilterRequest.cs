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
        public Dictionary<string, Expression<Func<T, object>>> Validate<T>(FilteryRequest filteryRequest, AbstractFilteryMapping<T> mappingConfiguration)
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
                .GetFiledValue<FilteryMapper<T>>(FilteryConstant.MapperFiledName)
                .GetFiledValue<Dictionary<string, Expression<Func<T, object>>>>(FilteryConstant.MappingListFieldName);
            
            foreach (var filterItem in filteryRequest.AndFilters)
            {
                if (!mappings.ContainsKey(filterItem.TargetFieldName.ToLowerInvariant()))
                {
                    throw new NotConfiguredFilterMappingException(filterItem.TargetFieldName.ToLowerInvariant());
                }
            }
            
            foreach (var filterItem in filteryRequest.AndFilters)
            {
                if (!mappings.ContainsKey(filterItem.TargetFieldName.ToLowerInvariant()))
                {
                    throw new NotConfiguredFilterMappingException(filterItem.TargetFieldName.ToLowerInvariant());
                }
            }
            
            foreach (var orderOperation in filteryRequest.OrderOperations)
            {
                if (!mappings.ContainsKey(orderOperation.Key.ToLowerInvariant()))
                {
                    throw new NotConfiguredOrderException(orderOperation.Key.ToLowerInvariant());
                }
            }

            return mappings;
        }
    }
}