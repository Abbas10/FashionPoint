using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Ecommerce.API.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;
            var userId = httpContext.User.Claims.SingleOrDefault(x => x.Type == "id");
            return userId?.Value;

        }
        public static string GetRole(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;
            return httpContext.User.Claims.Single(x => x.Type == ClaimTypes.Role).Value;
        }
    }
}
