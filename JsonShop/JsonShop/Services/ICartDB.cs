using JsonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Services
{
    public interface ICartDB
    {
        List<Cart> Carts{get;}
        Cart GetCartByUserId(int userId);
        void DeleteAllItems(int userId);
        void UpdateCart(Cart cart);
    }
}
