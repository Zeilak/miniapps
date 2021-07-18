using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Services
{
    public interface IUserDB
    {
        void AddUser(int id);
        bool TryUser(int id);
        int UsersCount();
    }
}
