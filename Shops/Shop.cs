using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Shops
{
    public class Shop
    {
        private static int _id = 1;

        private readonly List<ProductInShop> _catalog;

        public Shop()
        {
            Name = string.Empty;
            Address = string.Empty;
            Id = -1;
            Money = -1;
            _catalog = new List<ProductInShop>();
        }

        public Shop(string name, string address)
        {
            Name = name;
            Address = address;
            Id = _id;
            _id++;
            Money = 0;
            _catalog = new List<ProductInShop>();
        }

        public string Name
        {
            get;
            private set;
        }

        public string Address
        {
            get;
            private set;
        }

        public int Id
        {
            get;
        }

        public int Money
        {
            get;
            private set;
        }

        public void Sell(int price, int id, int amount)
        {
            Money += price;
            _catalog[id].Amount -= amount;
        }

        public void Supply(List<ProductInShop> products)
        {
            foreach (ProductInShop product in products)
            {
                ProductInShop tempProduct = _catalog.FirstOrDefault(temp => product.Id == temp.Id);
                if (tempProduct == null)
                {
                    _catalog.Add(product);
                    continue;
                }

                tempProduct.Amount += product.Amount;
            }
        }

        public void ChangePrice(int id, int newPrice)
        {
            _catalog[id].Price = newPrice;
        }

        public IReadOnlyList<ProductInShop> GetCatalog()
        {
            return _catalog.AsReadOnly();
        }
    }
}