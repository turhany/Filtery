using System;
using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace Filtery.Constants
{
    public static class FilteryQueryMarker
    {
        public static readonly string filterStringMarker = null;

        public static readonly int filterIntMarker = 0;
        public static readonly int? filterNullableIntMarker = null;

        public static readonly long filterLongMarker = 0;
        public static readonly long? filterNullableLongMarker = null;

        public static readonly DateTime filterDateTimeMarker = DateTime.Now;
        public static readonly DateTime? filterNullableDateTimeMarker = null;

        public static readonly bool filterBooleanMarker = true;
        public static readonly bool? filterNullableBooleanMarker = null;

        internal static readonly List<string> ParameterCompareList = new List<string>
        {
            "FilteryQueryMarker.filterStringMarker",
            "FilteryQueryMarker.filterIntMarker",
            "FilteryQueryMarker.filterNullableIntMarker",
            "FilteryQueryMarker.filterLongMarker",
            "FilteryQueryMarker.filterNullableLongMarker",
            "FilteryQueryMarker.filterDateTimeMarker",
            "FilteryQueryMarker.filterNullableDateTimeMarker",
            "FilteryQueryMarker.filterBooleanMarker",
            "FilteryQueryMarker.filterNullableBooleanMarker"
        };
    }
}