using System.Linq;
using Ecommerce.Web.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Ecommerce.Model;
using DataTables.AspNetCore.Mvc.Binder;
using System.Collections.Generic;

namespace Ecommerce.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CategoryController : Controller
    {
        #region Declaration
        private readonly CategoryProxy _proxy;
        #endregion

        #region Constructor
        public CategoryController(ILogger<HomeController> logger, IConfiguration config, IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            var token = accessor.HttpContext.User.Claims.First(x => x.Type == "token").Value;
            this._proxy = new CategoryProxy(config, httpClient, token);
        }
        #endregion

        #region Methods
        public IActionResult List()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            if (id != null) {
                var category = _proxy.Get(id.Value);
                return View(category);
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateUpdate(CategoryRequest request)
        {
            bool response = false;
            if (request.Id > 0)
            {
                response = _proxy.UpdateCategory(request.Id, request);
                if(response)
                    return Redirect("/category/list");
            }
            else { 
                response = _proxy.CreateCategory(request);
                if(response)
                    return Redirect("/category/list");
            }

            ViewBag.Message = (!response)?"Bad Request": string.Empty;

            return View(request);
        }
        public IActionResult Delete(int id)
        {
            _proxy.DeleteCategory(id);
            return Redirect("/category/list");
        }
        [HttpGet]
        public IActionResult GetCategoryList([DataTablesRequest] DataTablesRequest dataRequest)
        {
            IEnumerable<CategoryRequest> categories = _proxy.GetAll();
            int recordsTotal = categories.Count();
            int recordsFilterd = recordsTotal;

            //if (!string.IsNullOrEmpty(dataRequest.Search?.Value))
            //{
            //    categories = categories.Where(e => e.CategoryName.Contains(dataRequest.Search.Value));
            //    recordsFilterd = categories.Count();
            //}
            categories = categories.Skip(dataRequest.Start).Take(dataRequest.Length);
            var json = Json(categories
                .Select(e => new
                {
                    Id = e.Id,
                    CategoryName = e.CategoryName,
                    IsActive = e.IsActive
                })
                .ToDataTablesResponse(dataRequest, recordsTotal, recordsFilterd));
            return json;
        }
        #endregion
    }
}