using JsonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Services
{
    public class ProductDB : IProductDB
    {
        private static List<Product> _products = new List<Product>
        {
            new Product(0, "GTA V", "action-adventure game, 2013", 20.5),
            new Product(1, "Sid Meier’s Civilization V", "strategy game, 2010", 10.3),
            new Product(2, "Age of Empires II: Definitive Edition", "strategy game, 2019", 9.99),
            new Product(3, "Cities: skylines", "urban strategy game, 2015", 8.99)
        };
        public List<Product> Products
        {
            get { return _products; }
        }
        public Product GetProductById(int id)
        {
            int index = _products.FindIndex(product => product.Id == id);

            if (index == -1)
                throw new Exception("Incorrect ID");

            return _products[index];
        }
    }
}
