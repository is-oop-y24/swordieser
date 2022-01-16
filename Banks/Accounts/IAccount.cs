using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Banks.Banks;
using Banks.Transactions;

namespace Banks.Accounts
{
    public interface IAccount
    {
        public BankConditions Conditions { get; }
        public long Id { get; }
        public double Balance { get; }
        public double Percent { get; }
        public double Commission { get; }
        public double CreditLimit { get; }
        public double MonthlyPercentage { get; }
        public double MonthlyCommission { get; }
        public double MaxWithdraw { get; }
        public double MaxTransfer { get; }
        public List<string> MessagesList { get; }
        public int TransactionId { get; }
        public DateTime AccountPeriod { get; }

        public void Replenishment(double amount);
        public void Withdraw(double amount);
        public void Update(string message);
        public void AddTransaction(ITransaction t);
        public void BalanceUpdate(DateTime dateTime);
        public ReadOnlyCollection<ITransaction> GetTransactionsHistory();

        public void SetMaxWithdraw(double amount);
        public void SetMaxTransfer(double amount);
    }
}