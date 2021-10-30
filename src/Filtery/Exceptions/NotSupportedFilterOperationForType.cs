using System;

namespace Filtery.Exceptions
{
    public class NotSupportedFilterOperationForType : FilteryBaseException
    {
        public NotSupportedFilterOperationForType(string message) : base(message) 
        {
            
        }
    }
}