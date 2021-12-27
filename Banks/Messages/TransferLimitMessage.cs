using Banks.Accounts;

namespace Banks.Messages
{
    public class TransferLimitMessage : IBankMessage
    {
        public string Message(double amount)
        {
            return $"Transfer limit has changed to {amount}";
        }
    }
}