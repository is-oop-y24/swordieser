using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops
{
    public class Manager
    {
        private readonly Dictionary<int, Shop> _shops;

        public Manager()
        {
            _shops = new Dictionary<int, Shop>();
            Products = new Dictionary<int, Product>();
        }

        public Dictionary<int, Product> Products
        {
            get;
        }

        public Shop RegisterShop(string name, string address)
        {
            if (_shops.Values.Any(shopTemp => shopTemp.Name.Equals(name) && shopTemp.Address.Equals(address)))
            {
                throw new Exception("This shop is already exists");
            }

            var shop = new Shop(name, address);
            _shops.Add(shop.Id, shop);
            return shop;
        }

        public Product RegisterProduct(string name)
        {
            if (Products.Values.Any(productTemp => productTemp.Name.Equals(name)))
            {
                throw new Exception("This product is already exists");
            }

            var product = new Product(name);
            Products.Add(product.Id, product);
            return product;
        }

        public void Purchase(Customer customer, Shop shop, int id, int amount)
        {
            if (shop.GetCatalog().ContainsKey(id))
            {
                if (amount <= shop.GetCatalog()[id].Amount)
                {
                    if (customer.Money >= amount * shop.GetCatalog()[id].Price)
                    {
                        int price = amount * shop.GetCatalog()[id].Price;
                        customer.Purchase(price);
                        shop.Sell(price, id, amount);
                    }
                    else
                    {
                        throw new Exception("You haven't enough money :(");
                    }
                }
                else
                {
                    throw new Exception("This shop hasn't enough of this");
                }
            }
            else
            {
                throw new Exception("This shop hasn't this");
            }
        }

        public Shop FindCheapestShop(Product product, int amount)
        {
            int minPrice = int.MaxValue;
            var cheapestShop = new Shop();

            foreach (Shop shop in
                from shop in _shops.Values
                where shop.GetCatalog().ContainsKey(product.Id)
                      && shop.GetCatalog()[product.Id].Amount >= amount
                      && shop.GetCatalog()[product.Id].Price * amount < minPrice
                select shop)
            {
                minPrice = shop.GetCatalog()[product.Id].Price * amount;
                cheapestShop = shop;
            }

            if (cheapestShop.Address.Equals(string.Empty))
            {
                throw new Exception("You can't buy this consignment (not enough product in every shop or there is " +
                                    "no shop with this product");
            }

            return cheapestShop;
        }
    }
}