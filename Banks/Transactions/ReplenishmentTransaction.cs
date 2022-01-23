using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions
{
    public class ReplenishmentTransaction : ITransaction
    {
        public ReplenishmentTransaction(IAccount recipient, double amount, int id, IAccount sender = null)
        {
            if (amount <= 0)
            {
                throw new InvalidTransactionAmountException();
            }

            Recipient = recipient;
            Sender = sender;
            Amount = amount;
            Id = id;
            recipient.Replenishment(amount);
        }

        public IAccount Sender { get; }
        public IAccount Recipient { get; }
        public double Amount { get; }

        public int Id { get; }
        public bool IsCanceled { get; private set; }

        public void Cancel()
        {
            if (IsCanceled)
            {
                throw new AlreadyCanceledTransactionException();
            }

            Recipient.Withdraw(Amount);
            IsCanceled = true;
        }
    }
}