using System;
using System.Collections.Generic;

namespace Filtery.Models
{
    public class FilteryResponse<T>
    {
        public IList<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount => (int)Math.Ceiling(TotalItemCount / (double)PageSize);
    }
}