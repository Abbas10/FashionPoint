using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
    public class UnitController : Controller
    {
        #region Declaration
        private readonly UnitProxy _proxy;
        #endregion

        #region Constructor
        public UnitController(ILogger<HomeController> logger, IConfiguration config, IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            var token = accessor.HttpContext.User.Claims.First(x => x.Type == "token").Value;
            this._proxy = new UnitProxy(config, httpClient, token);
        }
        #endregion

        #region Methods
        [HttpGet]
        public IActionResult List()
        {
            var list = _proxy.GetAll();
            return View();
        }
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            if (id != null)
            {
                var category = _proxy.Get(id.Value);
                return View(category);
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateUpdate(UnitRequest request)
        {
            bool response = false;
            if (request.Id > 0)
            {
                response = _proxy.UpdateUnit(request.Id, request);
                if (response)
                    return Redirect("/unit/list");
            }
            else
            {
                response = _proxy.CreateUnit(request);
                if (response)
                    return Redirect("/unit/list");
            }

            ViewBag.Message = (!response) ? "Bad Request" : string.Empty;

            return View(request);
        }
        public IActionResult Delete(int id)
        {
            _proxy.DeleteUnit(id);
            return Redirect("/unit/list");
        }
        [HttpGet]
        public IActionResult GetUnitList([DataTablesRequest] DataTablesRequest dataRequest)
        {
            IEnumerable<UnitRequest> categories = _proxy.GetAll();
            int recordsTotal = categories.Count();
            int recordsFilterd = recordsTotal;

            //if (!string.IsNullOrEmpty(dataRequest.Search?.Value))
            //{
            //    categories = categories.Where(e => e.UnitName.Contains(dataRequest.Search.Value));
            //    recordsFilterd = categories.Count();
            //}
            categories = categories.Skip(dataRequest.Start).Take(dataRequest.Length);
            var json = Json(categories
                .Select(e => new
                {
                    Id = e.Id,
                    UnitName = e.UnitName,
                    IsActive = e.IsActive
                })
                .ToDataTablesResponse(dataRequest, recordsTotal, recordsFilterd));
            return json;
        }
        #endregion
    }
}