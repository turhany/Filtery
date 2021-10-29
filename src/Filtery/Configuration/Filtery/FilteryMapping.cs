using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Filtery.Models.Filter;

namespace Filtery.Configuration.Filtery
{
    public class FilteryMapping<T>
    {
        public List<FilterOperation> FilterOperations { get; set; }
        public Expression<Func<T,bool>> Expression { get; set; }
    }
}