using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions
{
    public class CancelTransaction : ITransaction
    {
        public CancelTransaction(ITransaction transaction)
        {
            transaction.Cancel();
        }

        public IAccount Sender { get; }
        public IAccount Recipient { get; }
        public double Amount { get; }
        public int Id { get; }
        public bool IsCanceled { get; set; }
        public void Cancel()
        {
        }
    }
}