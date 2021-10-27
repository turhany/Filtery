using System;

namespace Filtery.Exceptions
{
    internal class NotConfiguredFilterMappingException : Exception
    {
        public NotConfiguredFilterMappingException(string message) : base(message)
        {
            
        }
    }
}