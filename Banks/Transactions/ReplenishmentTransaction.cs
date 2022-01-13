using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions
{
    public class ReplenishmentTransaction : Transaction
    {
        public ReplenishmentTransaction(IAccount recipient, double amount, int id, IAccount sender = null)
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