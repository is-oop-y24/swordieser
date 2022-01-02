using System;
using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions
{
    public class TransferTransaction : Transaction
    {
        public TransferTransaction(Account sender, Account recipient, double amount, int id)
            : base(sender, recipient, amount, id)
        {
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
    }
}