using System;

namespace Filtery.Exceptions
{
    public class MultipleFilterItemConfigurationException : FilteryBaseException
    {
        public MultipleFilterItemConfigurationException(string message) : base(message)
        {
            
        }
    }
}