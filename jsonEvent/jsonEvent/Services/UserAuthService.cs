
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jsonEvent.Services
{
    public class UserAuthService : IUserAuthService
    {
        private IUserDB _userDB;
        private int _userId;
        public UserAuthService(IUserDB userDB)
        {
            _userDB = userDB;
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
