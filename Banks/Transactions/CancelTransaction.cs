using Banks.Exceptions;

namespace Banks.Transactions
{
    public class CancelTransaction : Transaction
    {
        public CancelTransaction(Transaction transaction)
            : base(transaction.Sender, transaction.Recipient, transaction.Amount, transaction.Id)
        {
            if (transaction.IsCanceled)
            {
                throw new AlreadyCanceledTransactionException();
            }

            if (transaction.Sender == null)
            {
                transaction.Recipient.Withdraw(transaction.Amount);
            }
            else if (transaction.Recipient == null)
            {
                transaction.Sender.Withdraw(transaction.Amount);
            }
            else
            {
                transaction.Sender.Replenishment(transaction.Amount);
                transaction.Recipient.Withdraw(transaction.Amount);
            }
        }
    }
}