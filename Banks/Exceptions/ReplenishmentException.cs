using System;

namespace Banks.Exceptions
{
    public class ReplenishmentException : Exception
    {
        public ReplenishmentException() { }

        public ReplenishmentException(string message)
            : base(message)
        {
        }
    }
}