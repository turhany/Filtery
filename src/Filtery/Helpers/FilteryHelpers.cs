using Filtery.Models;

namespace Filtery.Helpers
{
    public static class FilteryHelpers
    {
        public static FilteryRequest GetDefaultFilteryRequest() 
            => new FilteryRequest()
            {
                PageNumber = 1,
                PageSize = 10
            };

        public static FilteryRequest GetDefaultFilteryRequest(int pageSize)
            => new FilteryRequest()
            {
                PageNumber = 1,
                PageSize = pageSize
            };
    }
}
