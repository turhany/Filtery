using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Filtery.Configuration.Filtery
{
    public class FilteryMappingItem<T>
    {
        private Expression<Func<T, object>> _orderExpression;
        private Func<T, object> _compiledOrderExpression;

        public List<FilteryMapping<T>> FilteryMappings { get; set; }

        public Expression<Func<T, object>> OrderExpression
        {
            get => _orderExpression;
            set
            {
                _orderExpression = value;
                _compiledOrderExpression = null;
            }
        }

        public Func<object, object> ValueDecorator { get; set; }

        internal Func<T, object> GetCompiledOrderExpression()
        {
            return _compiledOrderExpression ?? (_compiledOrderExpression = _orderExpression?.Compile());
        }
    }
}