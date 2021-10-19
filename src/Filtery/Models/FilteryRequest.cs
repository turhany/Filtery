using System.Collections.Generic;
using Filtery.Models.Filter;
using Filtery.Models.Order;

namespace Filtery.Models
{
    public class FilteryRequest
    {
        public List<FilterItem> AndFilters { get; set; }
        public List<FilterItem> OrFilters { get; set; }

        public Dictionary<string, OrderOperation> OrderOperations { get; set; }

        public long PageNumber { get; set; }
        public long PageSize { get; set; }
    }
}