using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce.Web.Helpers
{
    public class EcommerceWebFilter: IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is EcommerceWebException exception)
            {
                context.HttpContext.Response.Clear();
                switch ((HttpStatusCode)exception.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        context.Result = new RedirectResult("/account/login");
                        context.ExceptionHandled = true;
                        break;
                    case HttpStatusCode.Forbidden:
                        context.Result = new RedirectResult("/home/error?message=Access Denied");
                        context.ExceptionHandled = true;
                        break;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
