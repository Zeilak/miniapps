using JsonShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Services
{
    public interface IProductDB
    {
        List<Product> Products { get; }
        Product GetProductById(int id);
    }
}
