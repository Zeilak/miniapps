using JsonShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductDB _productDB;
        public CatalogController(IProductDB productDB)
        {
            _productDB = productDB;
        }

        [HttpGet]
        public JsonResult All()
        {
            return Json(_productDB.Products);
        }

        [HttpGet]
        public JsonResult Id(int productId)
        {
            return Json(_productDB.GetProductById(productId));
        }
    }
}
