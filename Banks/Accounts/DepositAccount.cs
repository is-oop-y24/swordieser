using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Banks.Banks;
using Banks.Transactions;

namespace Banks.Accounts
{
    public class DepositAccount : IAccount
    {
        private readonly List<ITransaction> _transactionHistory;

        public DepositAccount(
            long id,
            BankConditions conditions,
            double startBalance,
            DateTime endOfAccount)
        {
            Id = id;
            Percent = conditions.ChooseDepositPercent(startBalance);
            MaxTransfer = conditions.MaxTransfer;
            MaxWithdraw = conditions.MaxWithdraw;
            Balance = startBalance;
            AccountPeriod = endOfAccount;
            _transactionHistory = new List<ITransaction>();
            MessagesList = new List<string>();
            Commission = 0;
            CreditLimit = 0;
        }

        public BankConditions Conditions { get; }
        public long Id { get; }
        public double Balance { get; private set; }
        public double Percent { get; private set; }
        public double Commission { get; }
        public double CreditLimit { get; }
        public double MonthlyPercentage { get; private set; }
        public double MonthlyCommission { get; private set; }
        public double MaxWithdraw { get; private set; }
        public double MaxTransfer { get; private set; }
        public List<string> MessagesList { get; }
        public int TransactionId { get; private set; } = 1;
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