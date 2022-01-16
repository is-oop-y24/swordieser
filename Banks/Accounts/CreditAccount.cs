using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Banks.Banks;
using Banks.Transactions;

namespace Banks.Accounts
{
    public class CreditAccount : IAccount
    {
        private readonly List<ITransaction> _transactionHistory;

        public CreditAccount(
            long id,
            BankConditions conditions,
            double startBalance)
        {
            Id = id;
            MaxTransfer = conditions.MaxTransfer;
            MaxWithdraw = conditions.MaxWithdraw;
            Balance = startBalance;
            CreditLimit = conditions.CreditLimit;
            Commission = conditions.Commission;
            _transactionHistory = new List<ITransaction>();
            MessagesList = new List<string>();
            Conditions = conditions;
        }

        public BankConditions Conditions { get; }
        public long Id { get; }
        public double Balance { get; private set; }
        public double Percent { get; }
        public double Commission { get; private set; }
        public double CreditLimit { get; private set; }

        public double MonthlyPercentage { get; private set; }
        public double MonthlyCommission { get; private set; }
        public double MaxWithdraw { get; private set; }
        public double MaxTransfer { get; private set; }
        public List<string> MessagesList { get; }
        public int TransactionId { get; private set; }
        public DateTime AccountPeriod { get; }

        public void Replenishment(double amount)
        {
            Balance += amount;
        }

        public void Withdraw(double amount)
        {
            Balance -= amount;
        }

        public void Update(string message)
        {
            MessagesList.Add(message);
        }

        public void AddTransaction(ITransaction t)
        {
            _transactionHistory.Add(t);
            TransactionId++;
        }

        public void BalanceUpdate(DateTime dateTime)
        {
            DateTime date = DateTime.Today;
            double daysUntilEnd = dateTime.Subtract(date).TotalDays;

            if (Commission != 0 && Balance < 0)
            {
                for (int i = 0; i < daysUntilEnd; i++)
                {
                    MonthlyCommission += Commission;
                    date = date.AddDays(1);

                    if (date.Day == 1)
                    {
                        Withdraw(MonthlyCommission);
                        MonthlyCommission = 0;
                    }
                }
            }
        }

        public ReadOnlyCollection<ITransaction> GetTransactionsHistory()
        {
            return _transactionHistory.AsReadOnly();
        }

        public void SetMaxWithdraw(double amount)
        {
            MaxWithdraw = amount;
        }

        public void SetMaxTransfer(double amount)
        {
            MaxTransfer = amount;
        }
    }
}