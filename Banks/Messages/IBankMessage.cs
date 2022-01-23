using Banks.Accounts;

namespace Banks.Messages
{
    public interface IBankMessage
    {
        public string Message(double amount);
    }
}