using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Models
{
    public class CartItem : ICloneable
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public CartItem() { }
        public CartItem(int productId, int count)
        {
            ProductId = productId;
            Count = count;
        }
        public object Clone()
        {
            return new CartItem(ProductId, Count);
        }
    }
}
