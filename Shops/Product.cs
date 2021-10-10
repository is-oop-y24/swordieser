using System.Security.Cryptography;

namespace Shops
{
    public class Product
    {
        private static int _id = 1;

        public Product()
        {
            Name = string.Empty;
            Id = -1;
        }

        public Product(string name)
        {
            Name = name;
            Id = _id;
            _id++;
        }

        public string Name
        {
            get;
            protected set;
        }

        public int Id
        {
            get;
            protected set;
        }
    }
}