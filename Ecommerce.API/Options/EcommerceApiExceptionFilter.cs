using Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;

namespace Ecommerce.WebService.Options
{
    public class EcommerceApiExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(exception.Value)
                {
                    StatusCode = exception.Status,
                };
                context.ExceptionHandled  = true;
            }
            else if(context.Exception is Exception ex)
            {
                string stringData = JsonConvert.SerializeObject(new ServiceDataWrapper<string> {
                    Error = new string[] { ex.Message },
                    value = null
                });
                
                context.Result = new ObjectResult(System.Net.HttpStatusCode.InternalServerError)
                {
                    Value = stringData
                };
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
