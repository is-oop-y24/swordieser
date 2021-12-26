namespace Banks.Accounts
{
    public class AccountBuilder
    {
        private Account _account;

        public AccountBuilder(long id)
        {
            _account = new Account(id);
        }

        public AccountBuilder SetStartBalance(double amount)
        {
            _account.Replenishment(amount);
            return this;
        }

        public AccountBuilder SetPercent(double percent)
        {
            _account.Percent = percent;
            return this;
        }

        public AccountBuilder SetCommission(double commission)
        {
            _account.Commission = commission;
            return this;
        }

        public AccountBuilder SetCreditLimit(double amount)
        {
            _account.CreditLimit = amount;
            return this;
        }

        public Account Build()
        {
            return _account;
        }
    }
}