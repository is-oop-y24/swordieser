using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Banks.Accounts;

namespace Banks.Clients
{
    public class Person
    {
        private List<IAccount> _accounts;

        public Person(string name, string surname, string address, long passport)
        {
            _accounts = new List<IAccount>();
            Name = name;
            Surname = surname;
            Address = address;
            Passport = passport;
            CheckDoubtfulness();
        }

        public string Name { get; }

        public string Surname { get; }

        private string Address { get; set; }

        private long Passport { get; set; }

        private bool Doubtful { get;  set; } = true;

        public void AddNewAccount(IAccount account)
        {
            _accounts.Add(account);
        }

        public ReadOnlyCollection<IAccount> GetAccounts()
        {
            return _accounts.AsReadOnly();
        }

        public void SetAddress(string address)
        {
            Address = address;
            CheckDoubtfulness();
        }

        public void SetPassport(long passport)
        {
            Passport = passport;
            CheckDoubtfulness();
        }

        public void CheckDoubtfulness()
        {
            if (Address != string.Empty && Passport != 0)
            {
                Doubtful = false;
                foreach (IAccount account in _accounts)
                {
                    account.SetMaxWithdraw(0);
                    account.SetMaxTransfer(0);
                    Doubtful = false;
                }
            }
        }
    }
}