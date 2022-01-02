using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Banks.Accounts;
using Banks.Clients;
using Banks.Exceptions;
using Banks.Messages;
using Banks.Observers;
using Banks.Transactions;

namespace Banks.Banks
{
    public class Bank : INotifyObservable
    {
        private static int _banksId = 1;

        private static int _accountsCounter = 1;

        private List<INotifyObserver> _observers;

        private List<Account> _accounts;

        private double _percent;

        private double _commission;

        private double _creditLimit;

        private double _maxTransfer;

        private double _maxWithdraw;

        private Dictionary<double, double> _percentsBorders;

        public Bank(
            string name,
            double percent,
            double commission,
            double creditLimit,
            double maxWithdraw,
            double maxTransfer,
            Dictionary<double, double> percentsBorders)
        {
            Id = _banksId++;
            Name = name;
            _percent = percent;
            _commission = commission;
            _creditLimit = creditLimit;
            _maxWithdraw = maxWithdraw;
            _maxTransfer = maxTransfer;
            _percentsBorders = percentsBorders;
            _accounts = new List<Account>();
            _observers = new List<INotifyObserver>();
        }

        public int Id { get; }

        public string Name { get; }

        public void RegisterObserver(INotifyObserver observer)
        {
            if (_observers.Any(obs => obs == observer))
            {
                throw new AlreadyRegisteredObserverException();
            }

            _observers.Add(observer);
        }

        public void RemoveObserver(INotifyObserver observer)
        {
            if (_observers.All(obs => obs != observer))
            {
                throw new NotRegisteredObserverException();
            }

            _observers.Remove(observer);
        }

        public void SendNotify(List<INotifyObserver> observers, double amount, IBankMessage message)
        {
            foreach (INotifyObserver observer in observers)
            {
                observer.Update(message.Message(amount));
            }
        }

        public Account CreateDebitAccount(Person person, double startBalance)
        {
            Account account = Account.CreateBuilder()
                .SetId(_accountsCounter++)
                .SetPercent(_percent)
                .SetMaxTransfer(_maxTransfer)
                .SetMaxWithdraw(_maxWithdraw)
                .SetStartBalance(startBalance)
                .Build();

            _accounts.Add(account);
            person.AddNewAccount(account);

            return account;
        }

        public Account CreateDepositAccount(Person person, double startBalance, DateTime end)
        {
            Account account = Account.CreateBuilder()
                .SetId(_accountsCounter++)
                .SetPercent(ChooseDepositPercent(startBalance))
                .SetMaxTransfer(_maxTransfer)
                .SetMaxWithdraw(_maxWithdraw)
                .SetStartBalance(startBalance)
                .SetAccountPeriod(end)
                .Build();

            _accounts.Add(account);
            person.AddNewAccount(account);

            return account;
        }

        public double ChooseDepositPercent(double balance)
        {
            foreach (var pair in _percentsBorders.Where(pair => balance < pair.Value))
            {
                return pair.Key;
            }

            return _percentsBorders.Last().Key;
        }

        public Account CreateCreditAccount(Person person, double startBalance)
        {
            Account account = Account.CreateBuilder()
                .SetId(_accountsCounter++)
                .SetMaxTransfer(_maxTransfer)
                .SetMaxWithdraw(_maxWithdraw)
                .SetStartBalance(startBalance)
                .SetCreditLimit(_creditLimit)
                .SetCommission(_commission)
                .Build();

            _accounts.Add(account);
            person.AddNewAccount(account);

            return account;
        }

        public void SetMaxTransfer(double amount)
        {
            var observers = new List<INotifyObserver>();
            foreach (Account account in _accounts.Where(account => account.MaxTransfer != 0))
            {
                observers.Add(account);
                account.MaxTransfer = amount;
            }

            SendNotify(observers, amount, new TransferLimitMessage());
        }

        public void SetMaxWithdraw(double amount)
        {
            var observers = new List<INotifyObserver>();
            foreach (Account account in _accounts.Where(account => account.MaxWithdraw != 0))
            {
                observers.Add(account);
                account.MaxWithdraw = amount;
            }

            SendNotify(observers, amount, new WithdrawLimitMessage());
        }

        public void SetCreditLimit(double amount)
        {
            var observers = new List<INotifyObserver>();
            foreach (Account account in _accounts.Where(account => account.CreditLimit != 0))
            {
                observers.Add(account);
                account.CreditLimit = amount;
            }

            SendNotify(observers, amount, new CreditLimitMessage());
        }

        public void SetPercent(double amount)
        {
            var observers = new List<INotifyObserver>();
            foreach (Account account in _accounts.Where(account => account.Percent != 0))
            {
                observers.Add(account);
                account.Percent = amount;
            }

            SendNotify(observers, amount, new PercentMessage());
        }

        public void Replenishment(Account account, double amount)
        {
            var trans = new ReplenishmentTransaction(account, amount, account.TransactionId++);
            account.AddTransaction(trans);
        }

        public void Withdraw(Account account, double amount)
        {
            if (account.AccountPeriod != DateTime.MinValue && account.AccountPeriod < DateTime.Today)
            {
                throw new NotEndedDepositAccountException();
            }

            var trans = new WithdrawTransaction(account, amount, account.TransactionId++);
            account.AddTransaction(trans);
        }

        public void Transfer(Account sender, Account recipient, double amount)
        {
            if (sender.AccountPeriod != DateTime.MinValue && sender.AccountPeriod < DateTime.Today)
            {
                throw new NotEndedDepositAccountException();
            }

            var trans = new TransferTransaction(sender, recipient, amount, sender.TransactionId++);
            sender.AddTransaction(trans);
            recipient.AddTransaction(trans);
        }

        public void Cancellation(Account account, Transaction transaction)
        {
            var trans = new CancelTransaction(transaction);
            account.AddTransaction(trans);
        }

        public void UpdateBalance(DateTime dateTime)
        {
            foreach (Account account in _accounts.Where(account => account.AccountPeriod != DateTime.MinValue))
            {
                account.BalanceUpdate(dateTime);
            }
        }

        public ReadOnlyCollection<Account> GetAccounts()
        {
            return _accounts.AsReadOnly();
        }
    }
}