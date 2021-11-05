using Filtery.Models;
using Filtery.Pager;

namespace Filtery.Extensions
{
    public static class FilteryExtensions
    {
        public static Page GetPageInfo<T>(this FilteryResponse<T> response) => new Page()
        {
            PageNumber = response.PageNumber,
            PageSize = response.PageSize,
            TotalItemCount = response.TotalItemCount
        };
    }
}