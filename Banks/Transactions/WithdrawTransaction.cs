using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions
{
    public class WithdrawTransaction : ITransaction
    {
        public WithdrawTransaction(IAccount sender, double amount, int id, IAccount recipient = null)
        {
            if (amount <= 0)
            {
                throw new InvalidTransactionAmountException();
            }

            if (sender.MaxWithdraw != 0 && amount > sender.MaxWithdraw)
            {
                throw new WithdrawException(
                    $"Your account is doubtful so you can't withdraw more than {sender.MaxWithdraw}");
            }

            if (sender.Balance + sender.CreditLimit < amount)
            {
                throw new WithdrawException(
                    $"You haven't enough money: your balance is {sender.Balance} and your credit limit is {sender.CreditLimit}");
            }

            sender.Withdraw(amount);
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
            IsCanceled = true;
        }
    }
}