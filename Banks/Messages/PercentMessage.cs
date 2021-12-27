using Banks.Accounts;

namespace Banks.Messages
{
    public class PercentMessage : IBankMessage
    {
        public string Message(double amount)
        {
            return $"Percent has changed to {amount}";
        }
    }
}