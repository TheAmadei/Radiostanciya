using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Radiostanciya.Data;
using Radiostanciya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.Middleware
{
    public class InitMiddleware
    {
        private readonly RequestDelegate _next;
        public InitMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public Task Invoke(HttpContext context, ApplContext dbContext)
        {
            DbInitializer.Initialize(dbContext);
            return _next.Invoke(context);

        }
    }
    public static class DbInitializerExtensions
    {
        public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InitMiddleware>();
        }
    }
}
