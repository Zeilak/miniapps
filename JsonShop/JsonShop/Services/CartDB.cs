using JsonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Services
{
    public class CartDB : ICartDB
    {
        private static List<Cart> _carts = new List<Cart>();
        public List<Cart> Carts
        {
            get { return _carts; }
        }
        public Cart GetCartByUserId(int userId)
        {
            for(int i = 0; i < _carts.Count; i++)
            {
                if(_carts[i].UserId == userId)
                {
                    return (Cart)_carts[i].Clone(); // особенность реализации ДБ (как просто объект c#)
                }
            }
            return null;
        }
        public void UpdateCart(Cart cart)
        {
            for (int i = 0; i < _carts.Count; i++)
            {
                if (_carts[i].UserId == cart.UserId)
                {
                    _carts[i] = cart;
                    return;
                }
            }
            _carts.Add(cart);
        }
        public void DeleteAllItems(int userId)
        {
            for (int i = 0; i < _carts.Count; i++)
            {
                if (_carts[i].UserId == userId)
                {
                    _carts[i].DeleteAllItems();
                    return;
                }
            }
        }
    }
}
