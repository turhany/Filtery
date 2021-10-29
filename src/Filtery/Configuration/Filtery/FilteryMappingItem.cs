using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Filtery.Configuration.Filtery
{
    public class FilteryMappingItem<T>
    {
        public List<FilteryMapping<T>> FilteryMappings { get; set; }
        public Expression<Func<T,object>> OrderExpression { get; set; }
    }
}