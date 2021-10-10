using System.Collections.Generic;
using NUnit.Framework;
using Shops;

namespace Shops.Tests
{
    public class ShopsTest
    {
        private Manager _manager;


        [SetUp]
        public void Setup()
        {
            _manager = new Manager();
        }

        [Test]
        public void SupplyGoodsToShop()
        {
            Shop shop = _manager.RegisterShop("Magnit", "Dimitrova, 12");
            Product product1 = _manager.RegisterProduct("Beer");
            Product product2 = _manager.RegisterProduct("Meat");
            Product product3 = _manager.RegisterProduct("Tea");

            var products = new List<ProductInShop>
            {
                new ProductInShop(product1, 15, 10),
                new ProductInShop(product2, 5, 20),
                new ProductInShop(product3, 10, 100)
            };

            shop.Supply(products);

            foreach (ProductInShop product in products)
            {
                Assert.Contains(product, shop.GetCatalog().Values);
            }
        }

        [Test]
        public void ChangingPrice()
        {
            Shop shop = _manager.RegisterShop("Magnit", "Dimitrova, 12");
            Product product1 = _manager.RegisterProduct("Beer");
            Product product2 = _manager.RegisterProduct("Meat");
            Product product3 = _manager.RegisterProduct("Tea");

            var products = new List<ProductInShop>
            {
                new ProductInShop(product1, 15, 10),
                new ProductInShop(product2, 5, 20),
                new ProductInShop(product3, 10, 100)
            };

            shop.Supply(products);
            shop.ChangePrice(2, 25);

            Assert.AreEqual(25, shop.GetCatalog()[2].Price);
        }

        [Test]
        public void CheapestShop()
        {
            Shop shop1 = _manager.RegisterShop("Magnit", "Dimitrova, 12");
            Shop shop2 = _manager.RegisterShop("Dixie", "Beli Kuna, 10");
            Product product1 = _manager.RegisterProduct("Beer");
            Product product2 = _manager.RegisterProduct("Meat");
            Product product3 = _manager.RegisterProduct("Tea");

            var products1 = new List<ProductInShop>
            {
                new ProductInShop(product1, 15, 10),
                new ProductInShop(product2, 5, 20),
                new ProductInShop(product3, 10, 100)
            };

            var products2 = new List<ProductInShop>
            {
                new ProductInShop(product1, 15, 5),
                new ProductInShop(product2, 5, 35)
            };

            shop1.Supply(products1);
            shop2.Supply(products2);

            Assert.AreEqual(shop2, _manager.FindCheapestShop(_manager.Products[1], 2));
        }

        [Test]
        public void BuyingGoods()
        {
            Shop shop = _manager.RegisterShop("Magnit", "Dimitrova, 12");
            Product product1 = _manager.RegisterProduct("Beer");
            Product product2 = _manager.RegisterProduct("Meat");
            Product product3 = _manager.RegisterProduct("Tea");

            var products = new List<ProductInShop>
            {
                new ProductInShop(product1, 15, 10),
                new ProductInShop(product2, 5, 20),
                new ProductInShop(product3, 10, 100)
            };

            shop.Supply(products);
            var customer = new Customer("Me", 1000);
            int moneyBefore = customer.Money;
            _manager.Purchase(customer, shop, 1, 5);
            Assert.Less(customer.Money, moneyBefore);
        }
    }
}