using Banks.Accounts;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace Banks.Transactions
{
    public abstract class Transaction
    {
        public Transaction(Account sender, Account recipient, double amount, int id)
        {
            Sender = sender;
            Recipient = recipient;
            Amount = amount;
            Id = id;
        }

        public Account Sender { get; }

        public Account Recipient { get; }

        public double Amount { get; }

        public int Id { get; }

        public bool IsCanceled { get; private set; } = false;

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}