using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions
{
    public class WithdrawTransaction : Transaction
    {
        public WithdrawTransaction(IAccount sender, double amount, int id, IAccount recipient = null)
            : base(sender, recipient, amount, id)
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
    }
}