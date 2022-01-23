namespace Banks.Messages
{
    public class WithdrawLimitMessage : IBankMessage
    {
        public string Message(double amount)
        {
            return $"Withdraw limit has changed to {amount}";
        }
    }
}