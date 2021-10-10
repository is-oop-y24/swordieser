using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shops
{
    public class Shop
    {
        private static int _id = 1;

        public Shop()
        {
            Name = string.Empty;
            Address = string.Empty;
            Id = -1;
            Money = -1;
            Catalog = new Dictionary<int, ProductInShop>();
        }

        public Shop(string name, string address)
        {
            Name = name;
            Address = address;
            Id = _id;
            _id++;
            Money = 0;
            Catalog = new Dictionary<int, ProductInShop>();
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

        private Dictionary<int, ProductInShop> Catalog
        {
            get;
        }

        public void Sell(int price, int id, int amount)
        {
            Money += price;
            Catalog[id].Amount -= amount;
        }

        public void Supply(List<ProductInShop> products)
        {
            foreach (var product in products)
            {
                if (Catalog.ContainsKey(product.Id))
                {
                    Catalog[product.Id].Amount += product.Amount;
                }
                else
                {
                    Catalog.Add(product.Id, product);
                }
            }
        }

        public void ChangePrice(int id, int newPrice)
        {
            Catalog[id].Price = newPrice;
        }

        public ReadOnlyDictionary<int, ProductInShop> GetCatalog()
        {
            return new ReadOnlyDictionary<int, ProductInShop>(Catalog);
        }
    }
}