using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Banks.Transactions;

namespace Banks.Accounts
{
    public class DebitAccount : IAccount
    {
        private readonly List<Transaction> _transactionHistory;

        public DebitAccount(
            long id,
            double percent,
            double maxTransfer,
            double maxWithdraw,
            double startBalance)
        {
            Id = id;
            Percent = percent;
            MaxTransfer = maxTransfer;
            MaxWithdraw = maxWithdraw;
            Balance = startBalance;
            MessagesList = new List<string>();
            _transactionHistory = new List<Transaction>();
            AccountPeriod = DateTime.MinValue;
        }

        public long Id { get; }
        public double Balance { get; private set; }
        public double Percent { get; private set; }
        public double Commission { get; } = 0;
        public double CreditLimit { get; } = 0;
        public double MonthlyPercentage { get; private set; }
        public double MonthlyCommission { get; private set; }
        public double MaxWithdraw { get; private set; }
        public double MaxTransfer { get; private set; }
        public List<string> MessagesList { get; }
        public int TransactionId { get; } = 1;
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

        public void AddTransaction(Transaction t)
        {
            _transactionHistory.Add(t);
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
            else
            {
                for (int i = 0; i < daysUntilEnd; i++)
                {
                    int daysInYear = new GregorianCalendar().GetDaysInYear(date.Year);
                    MonthlyPercentage += Balance * Math.Round(Percent / daysInYear, 2);
                    date = date.AddDays(1);
                    if (date.Day == 1)
                    {
                        Replenishment(MonthlyPercentage);
                        MonthlyPercentage = 0;
                    }
                }
            }
        }

        public ReadOnlyCollection<Transaction> GetTransactionsHistory()
        {
            return _transactionHistory.AsReadOnly();
        }

        public void SetPercent(double amount)
        {
            Percent = amount;
        }

        public void SetCommission(double amount)
        {
        }

        public void SetCreditLimit(double amount)
        {
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