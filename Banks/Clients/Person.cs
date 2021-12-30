using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Banks.Accounts;

namespace Banks.Clients
{
    public class Person
    {
        private List<Account> _accounts;

        public Person(string name, string surname)
        {
            _accounts = new List<Account>();
            Name = name;
            Surname = surname;
        }

        public string Name { get; }

        public string Surname { get; }

        public string Address { get; set; } = string.Empty;

        public long Passport { get; set; } = 0;

        public bool Doubtful { get; set; } = true;

        public void AddNewAccount(Account account)
        {
            _accounts.Add(account);
        }

        public ReadOnlyCollection<Account> GetAccounts()
        {
            return _accounts.AsReadOnly();
        }

        public void CheckDoubtfulness()
        {
            if (Address != string.Empty && Passport != 0)
            {
                Doubtful = false;
                foreach (Account account in _accounts)
                {
                    account.MaxWithdraw = 0;
                    account.MaxTransfer = 0;
                }
            }
        }
    }
}