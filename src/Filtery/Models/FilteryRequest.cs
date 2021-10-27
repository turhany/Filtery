using System.Collections.Generic;
using Filtery.Constants;
using Filtery.Models.Filter;
using Filtery.Models.Order;
// ReSharper disable CollectionNeverUpdated.Global

namespace Filtery.Models
{
    public class FilteryRequest
    {
        public List<FilterItem> AndFilters { get; set; } = new List<FilterItem>();
        public List<FilterItem> OrFilters { get; set; } = new List<FilterItem>();

        public Dictionary<string, OrderOperation> OrderOperations { get; set; } = new Dictionary<string, OrderOperation>();

        public int PageNumber { get; set; } = FilteryConstant.DefaultPageNumber;
        public int PageSize { get; set; } = FilteryConstant.DefaultPageSize;
    }
}