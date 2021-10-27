using System;

namespace Filtery.Exceptions
{
    internal class NotConfiguredOrderException :Exception
    {
        public NotConfiguredOrderException(string message) : base(message)
        {
            
        }
    }
}