using JsonShop.Models;
using JsonShop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductDB _productDB;
        private readonly ICartDB _cartDB;
        private readonly IUserAuthService _auth;
        public CartController(IProductDB productDB, ICartDB cartDB, IUserAuthService auth)
        {
            _productDB = productDB;
            _cartDB = cartDB;
            _auth = auth;
        }
        private bool GetCart(out Cart cart, int userId)
        {
            cart = _cartDB.GetCartByUserId(userId);
            return cart == null ? false : true;
        }

        [HttpGet]
        public JsonResult CartPrice()
        {
            Cart cart;
            if(!GetCart(out cart, _auth.UserId))
            {
                return Json(0);
            }
            else
            {
                return Json(cart.Price(_productDB));
            }
        }
        [HttpGet]
        public JsonResult ProductsFullInfo()
        {
            Cart cart;
            if (!GetCart(out cart, _auth.UserId))
            {
                return Json(0);
            }
            else
            {
                return Json(cart.ProductsInCart(_productDB));
            }
        }

        [HttpGet]
        public IActionResult Buy()
        {

            _cartDB.DeleteAllItems(_auth.UserId);
            return Ok();
        }

        [HttpGet]
        public IActionResult hehe()
        {
            Cart cart = new Cart(_auth.UserId);
            cart.AddItem(1, 3);
            _cartDB.UpdateCart(cart);
            return Ok();
        }
    }
}
