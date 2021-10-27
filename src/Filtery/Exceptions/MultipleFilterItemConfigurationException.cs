using System;

namespace Filtery.Exceptions
{
    internal class MultipleFilterItemConfigurationException : Exception
    {
        public MultipleFilterItemConfigurationException(string message) : base(message)
        {
            
        }
    }
}