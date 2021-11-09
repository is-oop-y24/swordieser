using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops
{
    public class Manager
    {
        private readonly List<Shop> _shops;

        public Manager()
        {
            _shops = new List<Shop>();
            Products = new List<Product>();
        }

        public List<Product> Products
        {
            get;
        }

        public Shop RegisterShop(string name, string address)
        {
            if (_shops.Any(shopTemp => shopTemp.Name.Equals(name) && shopTemp.Address.Equals(address)))
            {
                throw new ShopAlreadyExistException();
            }

            var shop = new Shop(name, address);
            _shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string name)
        {
            if (Products.Any(productTemp => productTemp.Name.Equals(name)))
            {
                throw new ProductAlreadyExistException();
            }

            var product = new Product(name);
            Products.Add(product);
            return product;
        }

        public void Purchase(Customer customer, Shop shop, int id, int amount)
        {
            if (shop.GetCatalog().All(temp => temp.Id != id))
            {
                throw new ShopHasNotProductException();
            }

            if (amount > shop.GetCatalog().First(temp => temp.Id == id).Amount)
            {
                throw new ShopHasntEnoughProductException();
            }

            if (customer.Money < amount * shop.GetCatalog().First(temp => temp.Id == id).Price)
            {
                throw new NotEnoughMoneyException();
            }

            int price = amount * shop.GetCatalog().First(temp => temp.Id == id).Price;
            customer.Purchase(price);
            shop.Sell(price, id, amount);
        }

        public Shop FindCheapestShop(Product product, int amount)
        {
            int minPrice = int.MaxValue;

            var cheapestShop = new Shop();
            foreach (Shop shop in
                from shop in _shops
                where shop.GetCatalog().Any(temp => temp.Id == product.Id)
                where shop.GetCatalog().First(temp => temp.Id == product.Id).Amount >= amount
                where shop.GetCatalog().First(temp => temp.Id == product.Id).Price * amount < minPrice
                select shop)
            {
                minPrice = shop.GetCatalog()[product.Id].Price * amount;
                cheapestShop = shop;
            }

            return cheapestShop;
        }
    }
}