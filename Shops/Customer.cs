namespace Shops
{
    public class Customer
    {
        public Customer(string name, int money)
        {
            Name = name;
            Money = money;
        }

        public string Name
        {
            get;
        }

        public int Money
        {
            get;
            private set;
        }

        public void Purchase(int price)
        {
            Money -= price;
        }
    }
}