using System;

namespace Filtery.Exceptions
{
    public class NotSupportedFilterOperationForType : Exception
    {
        public NotSupportedFilterOperationForType(string message) : base(message) 
        {
            
        }
    }
}