using Asp.Net.Project.Yad2.Services;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Project.Yad2.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Cookieware
    {
        private readonly RequestDelegate Next;

        public Cookieware(RequestDelegate next)
        {
            Next = next;
        }

        public Task Invoke(HttpContext httpContext, IUserService userService)
        {
            // UserModel user;
            // string[] cookies = httpContext.Request.Cookies["AspProjectCookie"].Split(',');
            //if(cookies.Length==2)
            // {
            //     userService.GetUser(cookies[0], cookies[1], out user);
            // }
            if (httpContext.Request.Cookies.ContainsKey("AspProjectCookie"))
            {
                string cookies = httpContext.Request.Cookies["AspProjectCookie"];
                string[] cookieArray = cookies.Split(',');
                httpContext.Items.Add("CookieKey", userService.GetUser(cookieArray[0], cookieArray[1]));
            }
            return Next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CookiewareExtensions
    {
        public static IApplicationBuilder UseCookieware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Cookieware>();
        }
    }
}
