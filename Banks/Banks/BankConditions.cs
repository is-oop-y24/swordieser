using System.Collections.Generic;
using System.Linq;

namespace Banks.Banks
{
    public class BankConditions
    {
        public BankConditions(
            double percent,
            double commission,
            double creditLimit,
            double maxTransfer,
            double maxWithdraw,
            Dictionary<double, double> percentsBorders)
        {
            Percent = percent;
            Commission = commission;
            CreditLimit = creditLimit;
            MaxTransfer = maxTransfer;
            MaxWithdraw = maxWithdraw;
            PercentsBorders = percentsBorders;
        }

        public double Percent { get; internal set; }

        public double Commission { get; internal set; }

        public double CreditLimit { get; internal set; }

        public double MaxTransfer { get; internal set; }

        public double MaxWithdraw { get; internal set; }

        private Dictionary<double, double> PercentsBorders { get; }

        public double ChooseDepositPercent(double balance)
        {
            return PercentsBorders.FirstOrDefault(pair => balance < pair.Value).Key;
        }
    }
}