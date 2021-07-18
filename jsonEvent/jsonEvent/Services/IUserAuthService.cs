using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jsonEvent.Services
{
    /// <summary>
    /// используется для аутентификации
    /// с его помощью можно узнать id пользователя запроса в БД
    /// </summary>
    public interface IUserAuthService
    {
        /// <summary>
        /// id пользователя запроса в БД
        /// </summary>
        int UserId { get; }
        /// <summary>
        /// вызывается в middleware, если пользователь новый - создает запись в бд
        /// </summary>
        /// <param name="context"></param>
        void Auth(HttpContext context);
    }
}
