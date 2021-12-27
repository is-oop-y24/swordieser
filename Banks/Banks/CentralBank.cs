using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Banks.Banks
{
    public class CentralBank
    {
        private static CentralBank instance;

        private List<Bank> _banks;

        protected CentralBank()
        {
            Name = "Central Bank";
            _banks = new List<Bank>();
        }

        public string Name { get; }

        public static CentralBank GetInstance()
        {
            return instance ??= new CentralBank();
        }

        public Bank CreateBank(
            string name,
            double percent,
            double commission,
            double creditLimit,
            double maxWithdraw,
            double maxTransfer,
            Dictionary<double, double> percentsBorders)
        {
            var bank = new Bank(name, percent, commission, creditLimit, maxWithdraw, maxTransfer, percentsBorders);
            _banks.Add(bank);
            return bank;
        }

        public ReadOnlyCollection<Bank> GetBanks()
        {
            return _banks.AsReadOnly();
        }

        public void NotifyBanks(DateTime dateTime)
        {
            foreach (Bank bank in _banks)
            {
                bank.UpdateBalance(dateTime);
            }
        }
    }
}