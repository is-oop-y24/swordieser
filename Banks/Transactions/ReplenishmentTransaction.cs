using Banks.Accounts;

namespace Banks.Transactions
{
    public class ReplenishmentTransaction : Transaction
    {
        public ReplenishmentTransaction(Account recipient, double amount, int id, Account sender = null)
            : base(sender, recipient, amount, id)
        {
            recipient.Replenishment(amount);
        }
    }
}