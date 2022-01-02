using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Banks.Observers;
using Banks.Transactions;

namespace Banks.Accounts
{
    public class Account : INotifyObserver
    {
        private List<Transaction> _transactionsHistory;

        public Account()
        {
            _transactionsHistory = new List<Transaction>();
            MessagesList = new List<string>();
        }

        public long Id { get; set; }

        public double Balance { get; private set; }

        public double Percent { get; set; } = 0;

        public double Commission { get; set; }

        public double CreditLimit { get; set; } = 0;

        public double MonthlyPercentage { get; set; }

        public double MonthlyCommission { get; set; }

        public double MaxWithdraw { get; set; } = 0;

        public double MaxTransfer { get; set; } = 0;

        public List<string> MessagesList { get; }

        public int TransactionId { get; set; } = 1;

        public DateTime AccountPeriod { get; set; } = DateTime.MinValue;

        public static AccountBuilder CreateBuilder()
        {
            return new AccountBuilder();
        }

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
            _transactionsHistory.Add(t);
        }

        public void BalanceUpdate(DateTime dateTime)
        {
            var date = DateTime.Today;
            var daysUntilEnd = dateTime.Subtract(date).TotalDays;

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
            return _transactionsHistory.AsReadOnly();
        }
    }
}