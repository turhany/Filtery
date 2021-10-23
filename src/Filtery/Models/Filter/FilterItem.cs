﻿namespace Filtery.Models.Filter
{
    public class FilterItem
    {
        public string Value { get; set; }
        public string TargetFieldName { get; set; }
        public FilterOperation Operation { get; set; }
        public bool? CaseSensitive { get; set; }
    }
}