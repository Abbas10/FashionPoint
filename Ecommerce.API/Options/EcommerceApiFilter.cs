using Ecommerce.API.Extensions;
using Ecommerce.DAL.BL;
using Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;

namespace Ecommerce.WebService.Options
{
    public class EcommerceApiFilter : IActionFilter, IOrderedFilter
    {
        #region Declaration
        public readonly IIdentityService _service;
        #endregion

        #region Constructor
        public EcommerceApiFilter(IIdentityService service)
        {
            _service = service;
        }
        #endregion

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
            var userId = context.HttpContext.GetUserId();
            if(userId != null)
            {
                if (_service.IsUserLockedByAdmin(userId).Result)
                {
                    string stringData = JsonConvert.SerializeObject(new ServiceDataWrapper<string>
                    {
                        Error = new string[] { "User Locked" },
                        value = null,
                        ErrorCode = (short) System.Net.HttpStatusCode.Locked
                    });

                    context.Result = new ObjectResult(System.Net.HttpStatusCode.Locked)
                    {
                        Value = stringData
                    };
                }
            }
        }
    }
}
