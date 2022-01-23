using Banks.Accounts;

namespace Banks.Messages
{
    public class CreditLimitMessage : IBankMessage
    {
        public string Message(double amount)
        {
            return $"Credit limit has changed to {amount}";
        }
    }
}