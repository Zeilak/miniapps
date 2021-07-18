using JsonShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Models
{
    public class Cart : ICloneable
    {
        private List<CartItem> _items;
        private int _userId;
        public List<CartItem> Items
        {
            get { return _items; }
        }
        public int UserId
        {
            get { return _userId; }
        }
        public Cart(int userId)
        {
            _userId = userId;
            _items = new List<CartItem>();
        }
        private void CartEnum<T>(ref T result, Func<CartItem, Product, T, T> func, IProductDB productDB)
        {
            List<CartItem> items = new List<CartItem>(_items);
            for (int iDB = 0; iDB < productDB.Products.Count; iDB++)
            {
                for (int iCart = 0; iCart < items.Count; iCart++)
                {
                    if (items[iCart].ProductId == productDB.Products[iDB].Id)
                    {
                        result = func(items[iCart], productDB.Products[iDB], result);
                        items.RemoveAt(iCart);
                        break;
                    }
                }
                if (items.Count == 0)
                    break;
            }
        }
        private List<Product> ProductsInCartFunc(CartItem cartItem, Product product, List<Product> products)
        {
            products.Add(product);
            return products;
        }
        private double PriceFunc(CartItem cartItem, Product product, double price)
        {
            return price + cartItem.Count * product.Price;
        }
        public double Price(IProductDB productDB)
        {
            double price = 0;
            CartEnum<double>(ref price, PriceFunc, productDB);
            return price;
        }
        public List<Product> ProductsInCart(IProductDB productDB)
        {
            List<Product> products = new List<Product>();
            CartEnum<List<Product>>(ref products, ProductsInCartFunc, productDB);
            return products;
        }
        
        public object Clone()
        {
            Cart cart = new Cart(_userId);
            cart._items = new List<CartItem>();
            for(int i = 0; i < _items.Count; i++)
            {
                cart._items.Add((CartItem)_items[i].Clone());
            }
            return cart;
        }
        public void AddItem(int productId, int count)
        {
            bool newProductId = true;
            for(int i = 0; i < _items.Count; i++)
            {
                if(_items[i].ProductId == productId)
                {
                    newProductId = false;
                    _items[i].Count += count;
                    break;
                }
            }    
            if(newProductId)
            {
                _items.Add(new CartItem(productId, count));
            }
        }

        public void DeleteItem(int productId, int count)
        {
            bool newProductId = true;
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].ProductId == productId)
                {
                    newProductId = false;
                    _items[i].Count -= count;
                    if (_items[i].Count < 0)
                    {
                        _items[i].Count = 0;
                    }
                    break;
                }
            }
            if (newProductId)
            {
                throw new Exception("not found product id in cart");
            }
        }

        public void DeleteAllItems()
        {
            _items = new List<CartItem>();
        }
    }
}
