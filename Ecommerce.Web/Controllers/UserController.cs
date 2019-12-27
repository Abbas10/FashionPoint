using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using DataTables.AspNetCore.Mvc.Binder;
using Ecommerce.Model;
using Ecommerce.Web.Proxy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        #region Declaration
        private IdentityServiceProxy _proxy;
        private readonly string _token;
        #endregion

        #region Constructor
        public UserController(ILogger<HomeController> logger, IConfiguration config, IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            var token = accessor.HttpContext.User.Claims.First(x => x.Type == "token").Value;
            this._proxy = new IdentityServiceProxy(config, httpClient, token);
        }
        #endregion

        #region Methods
        public IActionResult List()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetUserList([DataTablesRequest] DataTablesRequest dataRequest)
        {
            IEnumerable<ApplicationUserRequest> users = _proxy.GetAllUser();
            int recordsTotal = users.Count();
            int recordsFilterd = recordsTotal;

            //if (!string.IsNullOrEmpty(dataRequest.Search?.Value))
            //{
            //    users = users.Where(e => e.Email.Contains(dataRequest.Search.Value));
            //    recordsFilterd = users.Count();
            //}
            users = users.Skip(dataRequest.Start).Take(dataRequest.Length);
            var json = Json(users
                .Select(e => new
                {
                    Id = e.Id,
                    UserName = e.UserName,
                    Email = e.Email,
                    Role = e.Role,
                    ContactNo = e.ContactNo,
                    IsLocked = e.IsLocked
                })
                .ToDataTablesResponse(dataRequest, recordsTotal, recordsFilterd));
            return json;
        }
        
        public IActionResult LockUnLock(string id)
        {
            _proxy.LockUnlock(id);
            return RedirectToAction("List");
        }
        #endregion
    }
}