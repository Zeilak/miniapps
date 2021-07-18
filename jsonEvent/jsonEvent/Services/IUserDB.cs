using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jsonEvent.Services
{
    /// <summary>
    /// бд с пользователями
    /// </summary>
    public interface IUserDB
    {
        void AddUser(int id);
        /// <summary>
        /// проверка: есть ли такой id в бд
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool TryUser(int id);
        int UsersCount();
    }
}
