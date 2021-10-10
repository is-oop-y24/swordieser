namespace Shops
{
    public class ProductInShop : Product
    {
        public ProductInShop(Product product, int amount, int price)
        {
            Name = product.Name;
            Id = product.Id;
            Amount = amount;
            Price = price;
        }

        public int Amount
        {
            get;
            set;
        }

        public int Price
        {
            get;
            set;
        }
    }
}