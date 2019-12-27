using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Helpers
{
    public class EcommerceWebExceptionHandler
    {
        private readonly RequestDelegate _next;

        public EcommerceWebExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var status = context.Response.StatusCode;
            if (status == 500)
            {
                context.Response.Clear();
                context.Response.Redirect("/home/error", true);
                return;
            }
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
