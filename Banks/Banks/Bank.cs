using System.Collections.Generic;
using Banks.Accounts;

namespace Banks.Banks
{
    public class Bank
    {
        private static int _banksId = 1;

        private List<Account> _accounts;

        private double _percent;

        private double _commission;

        private double _creditLimit;

        public Bank(
            string name,
            double percent,
            double commission,
            double creditLimit)
        {
            Id = _banksId++;
            Name = name;
            _percent = percent;
            _commission = commission;
            _creditLimit = creditLimit;
            _accounts = new List<Account>();
        }

        public int Id { get; }

        public string Name { get; }
    }
}