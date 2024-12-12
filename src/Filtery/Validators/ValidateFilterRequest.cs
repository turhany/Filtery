using System.Collections.Generic;
using System.Linq;
using Filtery.Configuration.Filtery;
using Filtery.Constants;
using Filtery.Exceptions;
using Filtery.Extensions;
using Filtery.Models;
using Filtery.Models.Filter;
using Filtery.Models.Order;

namespace Filtery.Validators
{
    public class ValidateFilterRequest
    {
        private readonly string OperationNotSupportMessage = "'{0}' operation not supported for '{1}'";

        public Dictionary<string, FilteryMappingItem<TEntity>> Validate<TEntity>(FilteryRequest filteryRequest, AbstractFilteryMapping<TEntity> mappingConfiguration)
        {
            if (mappingConfiguration == null)
            {
                throw new NullFilteryMappingException("No filtery mapping for build filter query");
            }

            if (filteryRequest == null)
            {
                throw new NullFilterRequestException("No filtery request for build filter query");
            }

            var mappings = mappingConfiguration
                .GetFieldValue<FilteryMapper<TEntity>>(FilteryConstant.MapperFiledName)
                .GetFieldValue<Dictionary<string, FilteryMappingItem<TEntity>>>(FilteryConstant.MappingListFieldName);

            foreach (var filterItem in filteryRequest.AndFilters)
            {
                ValidateFilterOperationSupport(mappings, filterItem);
            }

            foreach (var filterItem in filteryRequest.OrFilters)
            {
                ValidateFilterOperationSupport(mappings, filterItem);
            }

            foreach (var orderOperation in filteryRequest.OrderOperations)
            {
                ValidateOrderSupport(mappings, orderOperation);
            }

            return mappings;
        }

        #region Private Methods

        private void ValidateFilterOperationSupport<TEntity>(Dictionary<string, FilteryMappingItem<TEntity>> mappings, FilterItem filterItem)
        {
            if (!mappings.ContainsKey(filterItem.TargetFieldName.ToLowerInvariant()))
            {
                throw new NotConfiguredFilterMappingException(filterItem.TargetFieldName.ToLowerInvariant());
            }

            if (!mappings[filterItem.TargetFieldName.ToLowerInvariant()].FilteryMappings.SelectMany(p => p.FilterOperations).Contains(filterItem.Operation))
            {
                throw new NotSupportedFilterOperationForType(string.Format(OperationNotSupportMessage, filterItem.Operation.ToString(), filterItem.TargetFieldName));
            }
        }

        private void ValidateOrderSupport<TEntity>(Dictionary<string, FilteryMappingItem<TEntity>> mappings, KeyValuePair<string, OrderOperation> orderOperation)
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

        #endregion
    }
}