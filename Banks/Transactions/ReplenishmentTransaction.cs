using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions
{
    public class ReplenishmentTransaction : Transaction
    {
        public ReplenishmentTransaction(Account recipient, double amount, int id, Account sender = null)
            : base(sender, recipient, amount, id)
        {
            if (amount <= 0)
            {
                throw new InvalidTransactionAmountException();
            }

            recipient.Replenishment(amount);
        }
    }
}