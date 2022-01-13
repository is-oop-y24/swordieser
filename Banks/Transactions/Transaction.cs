using Banks.Accounts;

namespace Banks.Transactions
{
    public abstract class Transaction
    {
        protected Transaction(IAccount sender, IAccount recipient, double amount, int id)
        {
            Sender = sender;
            Recipient = recipient;
            Amount = amount;
            Id = id;
        }

        public IAccount Sender { get; }

        public IAccount Recipient { get; }

        public double Amount { get; }

        public int Id { get; }

        public bool IsCanceled { get; private set; }

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}