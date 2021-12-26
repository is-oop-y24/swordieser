using System;
using Banks.Exceptions;

namespace Banks.Accounts
{
    public class Account
    {
        public Account(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public double Balance { get; private set; }

        public double Percent { get; set; }

        public double Commission { get; set; }

        public double CreditLimit { get; set; }

        public double MonthlyPercentage { get; set; }

        public double MonthlyCommission { get; set; }

        public double MaxWithdraw { get; set; } = 0;

        public double MaxTransfer { get; set; } = 0;

        public void Replenishment(double amount)
        {
            Balance += amount;
        }

        public void Withdraw(double amount)
        {
            Balance -= amount;
        }
    }
}