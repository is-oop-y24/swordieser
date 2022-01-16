using Banks.Accounts;

namespace Banks.Transactions
{
    public interface ITransaction
    {
        public IAccount Sender { get; }

        public IAccount Recipient { get; }

        public double Amount { get; }

        public int Id { get; }

        public bool IsCanceled { get; }

        public void Cancel();
    }
}