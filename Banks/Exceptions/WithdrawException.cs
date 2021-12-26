using System;

namespace Banks.Exceptions
{
    public class WithdrawException : Exception
    {
        public WithdrawException() { }

        public WithdrawException(string message)
            : base(message)
        {
        }
    }
}