using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop.Services
{
    public interface IUserAuthService
    {
        int UserId { get; }
        void Auth(HttpContext context);
    }
}
