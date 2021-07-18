using JsonShop.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonShop
{
    public class AuthUserMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserAuthService auth)
        {
            auth.Auth(context);
            await _next.Invoke(context);
        }
    }
}
