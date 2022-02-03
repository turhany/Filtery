using System;
using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace Filtery.Constants
{
    public static class FilteryQueryValueMarker
    {
        public static readonly string FilterStringValue = null;
        
        public static readonly char FilterCharValue = '.';
        public static readonly char? FilterCharNullableValue = null;

        public static readonly short FilterShortValue = 0;
        public static readonly short? FilterNullableShortValue = null;
        
        public static readonly byte FilterByteValue = 0;
        public static readonly byte? FilterNullableByteValue = null;
        
        public static readonly int FilterIntValue = 0;
        public static readonly int? FilterNullableIntValue = null;

        public static readonly long FilterLongValue = 0;
        public static readonly long? FilterNullableLongValue = null;

        public static readonly float FilterFloatValue = 0;
        public static readonly float? FilterNullableFloatValue = null;

        public static readonly decimal FilterDecimalValue = 0;
        public static readonly decimal? FilterNullableDecimalValue = null;

        public static readonly double FilterDoubleValue = 0;
        public static readonly double? FilterNullableDoubleValue = null;

        public static readonly DateTime FilterDateTimeValue = DateTime.Now;
        public static readonly DateTime? FilterNullableDateTimeValue = null;

        public static readonly bool FilterBooleanValue = true;
        public static readonly bool? FilterNullableBooleanValue = null;
         
        internal static readonly List<string> ParameterCompareList = new List<string>
        {
            "FilteryQueryValueMarker.FilterStringValue",
            "FilteryQueryValueMarker.FilterCharValue",
            "FilteryQueryValueMarker.FilterCharNullableValue",
            "FilteryQueryValueMarker.FilterShortValue",
            "FilteryQueryValueMarker.FilterNullableShortValue",
            "FilteryQueryValueMarker.FilterByteValue",
            "FilteryQueryValueMarker.FilterNullableByteValue",
            "FilteryQueryValueMarker.FilterIntValue",
            "FilteryQueryValueMarker.FilterNullableIntValue",
            "FilteryQueryValueMarker.FilterLongValue",
            "FilteryQueryValueMarker.FilterNullableLongValue",
            "FilteryQueryValueMarker.FilterFloatValue",
            "FilteryQueryValueMarker.FilterNullableFloatValue",
            "FilteryQueryValueMarker.FilterDecimalValue",
            "FilteryQueryValueMarker.FilterNullableDecimalValue",
            "FilteryQueryValueMarker.FilterDoubleValue",
            "FilteryQueryValueMarker.FilterNullableDoubleValue",
            "FilteryQueryValueMarker.FilterDateTimeValue",
            "FilteryQueryValueMarker.FilterNullableDateTimeValue",
            "FilteryQueryValueMarker.FilterBooleanValue",
            "FilteryQueryValueMarker.FilterNullableBooleanValue"
        };
    }
}