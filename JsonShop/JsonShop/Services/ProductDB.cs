using JsonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Services
{
    public class ProductDB : IProductDB
    {
        private List<Product> _products;
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
