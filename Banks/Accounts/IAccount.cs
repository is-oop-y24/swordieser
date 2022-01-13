using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Banks.Transactions;

namespace Banks.Accounts
{
    public interface IAccount
    {
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
        public void AddTransaction(Transaction t);
        public void BalanceUpdate(DateTime dateTime);
        public ReadOnlyCollection<Transaction> GetTransactionsHistory();

        public void SetPercent(double amount);
        public void SetCommission(double amount);
        public void SetCreditLimit(double amount);
        public void SetMaxWithdraw(double amount);
        public void SetMaxTransfer(double amount);
    }
}