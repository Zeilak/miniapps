using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jsonEvent.Services
{
    public class UserDB : IUserDB
    {
        private static List<User> _users = new List<User>();
        public void AddUser(int id)
        {
            User user = new User();
            user.Id = id;
            _users.Add(user);
        }

        public bool TryUser(int id)
        {
            for(int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Id == id)
                    return true;
            }
            return false;
        }
        public int UsersCount()
        {
            return _users.Count;
        }
    }
}
