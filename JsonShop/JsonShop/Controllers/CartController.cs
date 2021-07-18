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
        public JsonResult Price()
        {
            Cart cart;
            if(!GetCart(out cart, _auth.UserId))
            {
                throw new Exception("User or cart not found");
            }
            else
            {
                return Json(cart.Price(_productDB));
            }
        }
        [HttpGet]
        public JsonResult Products()
        {
            Cart cart;
            if (!GetCart(out cart, _auth.UserId))
            {
                throw new Exception("User or cart not found");
            }
            else
            {
                return Json(cart.ProductsInCart(_productDB));
            }
        }
        [HttpGet]
        public JsonResult Get()
        {
            Cart cart;
            if (!GetCart(out cart, _auth.UserId))
            {
                throw new Exception("User or cart not found");
            }
            else
            {
                return Json(cart.Items);
            }
        }
        public JsonResult Add(int productId, int count)
        {
            Cart cart;
            if (!GetCart(out cart, _auth.UserId))
                throw new Exception("User or cart not found");

            cart.AddItem(productId, count);
            _cartDB.UpdateCart(cart);
            return Json(cart);
        }
        public JsonResult Delete(int productId, int count)
        {
            Cart cart;
            if (!GetCart(out cart, _auth.UserId))
                throw new Exception("User or cart not found");

            cart.DeleteItem(productId, count);
            _cartDB.UpdateCart(cart);
            return Json(cart);
        }
        [HttpGet]
        public IActionResult Buy()
        {
            // тут должна быть оплата, добавление в список заказов и т.д.

            _cartDB.DeleteAllItems(_auth.UserId);
            return Ok();
        }

        [HttpGet]
        public IActionResult test()
        {
            Cart cart = new Cart(_auth.UserId);
            cart.AddItem(1, 3);
            _cartDB.UpdateCart(cart);
            return Ok();
        }
    }
}
