using JsonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Services
{
    /// <summary>
    /// хранение корзин, у одного пользователя не больше одной корзины, нет логики
    /// </summary>
    public interface ICartDB
    {
        List<Cart> Carts{get;}
        Cart GetCartByUserId(int userId);
        void DeleteAllItems(int userId);
        void UpdateCart(Cart cart);
    }
}
