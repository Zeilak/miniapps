using JsonShop.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Services
{
    public class UserAuthService : IUserAuthService
    {
        private IUserDB _userDB;
        private ICartDB _cartDB;
        private int _userId;
        public UserAuthService(IUserDB userDB, ICartDB cartDB)
        {
            _userDB = userDB;
            _cartDB = cartDB;
        }
        private void AddNewUser(HttpContext context)
        {
            // не оч хороший способ...
            int newUserId = _userDB.UsersCount();
            while (true)
            {
                if (!_userDB.TryUser(newUserId))
                    break;
            }
            context.Response.Cookies.Append("UserId", newUserId.ToString());
            _userDB.AddUser(newUserId);
            _cartDB.UpdateCart(new Cart(newUserId));
            _userId = newUserId;
        }
        public void Auth(HttpContext context)
        {
            //
            var value = context.Request.Cookies["UserId"];
            int uid = Int32.Parse(value);
            if (value == null || _userDB.TryUser(uid))
            {
                AddNewUser(context);
            }
            else
            {
                _userId = uid;
            }
        }
        public int UserId
        {
            get { return _userId; }
        }
    }
}
