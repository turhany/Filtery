using System;

namespace Filtery.Exceptions
{
    public class NotConfiguredOrderException :FilteryBaseException
    {
        public NotConfiguredOrderException(string message) : base(message)
        {
            
        }
    }
}