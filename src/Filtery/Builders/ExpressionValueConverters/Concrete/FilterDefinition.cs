using System.Collections.Generic;
using Filtery.Models.Filter;

namespace Filtery.Builders.ExpressionValueConverters.Concrete
{
    internal class FilterDefinition
    {
        public static FilterOperation DefaultFilterOperator = FilterOperation.Equal;

        /// <summary>
        /// This value is needed to prevent filter.js/clearEmptyFilters function from removing Empty selections for Nullable types.
        /// This is required for distinguishing Empty (filter by null) choice from Not Set (no filter) choice.
        /// </summary>
        internal const string NullSeedValue = "62080afb-21de-4caf-ab62-d9e34cbf73e6";
        public string FieldName { get; set; }
        public KeyValuePair<FilterOperation, string>[] Operators { get; set; }
        public string PropertyName { get; set; }
        public FilterPropertyType PropertyType { get; set; }
        public KeyValuePair<string, string>[] Seed { get; set; }
    }
}