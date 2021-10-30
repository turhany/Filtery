using System;

namespace Filtery.Exceptions
{
    public class NullFilterRequestException :FilteryBaseException
    {
        public NullFilterRequestException(string message) : base(message)
        {
            
        }
    }
}