using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions
{
    public class TransferTransaction : ITransaction
    {
        public TransferTransaction(IAccount sender, IAccount recipient, double amount, int id)
        {
            Recipient = recipient;
            Sender = sender;
            Amount = amount;
            Id = id;
            if (amount <= 0)
            {
                throw new InvalidTransactionAmountException();
            }

            if (sender.MaxWithdraw != 0 && amount > sender.MaxWithdraw)
            {
                throw new ReplenishmentException(
                    $"Your account is doubtful so you can't transfer more than {sender.MaxTransfer}");
            }

            if (sender.Balance + sender.CreditLimit < amount)
            {
                throw new ReplenishmentException(
                    $"You haven't enough money: your balance is {sender.Balance} and your credit limit is {sender.CreditLimit}");
            }

            sender.Withdraw(amount);
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

            Sender.Replenishment(Amount);
            Recipient.Withdraw(Amount);
            IsCanceled = true;
        }
    }
}