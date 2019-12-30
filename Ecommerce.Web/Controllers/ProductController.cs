using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DataTables.AspNetCore.Mvc.Binder;
using Ecommerce.Model;
using Ecommerce.Web.Proxy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProductController : Controller
    {
        #region Declaration
        private readonly ProductProxy _productProxy;
        private readonly CategoryProxy _categoryProxy;
        private readonly UnitProxy _unitProxy;

        public IWebHostEnvironment _hostingEnvironment { get; }
        #endregion

        #region Constructor
        public ProductController(ILogger<HomeController> logger
                                , IConfiguration config, IHttpClientFactory httpClient, IHttpContextAccessor accessor
                                , IWebHostEnvironment hostingEnvironment)
        {
            var token = accessor.HttpContext.User.Claims.First(x => x.Type == "token").Value;
            this._productProxy = new ProductProxy(config, httpClient, token);
            this._categoryProxy = new CategoryProxy(config, httpClient, token);
            this._unitProxy = new UnitProxy(config, httpClient, token);
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Catalog()
        {
            var products = _productProxy.GetAll();
            return View(products);
        }
        public IActionResult Detail(int id) {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            SetPrerequisite();
            if (id != null)
            {
                var product = _productProxy.Get(id.Value);
                return View(product);
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateUpdate(ProductRequest request, IFormFile file)
        {
            #region Save Product Photo
            if (file != null)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath + "\\images");
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                file.CopyTo(new FileStream(filePath, FileMode.Create));
                request.Photo = uniqueFileName;
            }
            #endregion

            var response = (request.Id > 0) ? _productProxy.UpdateProduct(request.Id, request) : _productProxy.CreateProduct(request);
            if (response)
                return Redirect("/product/list");
            
            SetPrerequisite();
            return View(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            _productProxy.DeleteProduct(id);
            return RedirectToAction("list");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRequest"></param>
        /// <returns></returns>
        public IActionResult GetProductList([DataTablesRequest] DataTablesRequest dataRequest)
        {
            IEnumerable<ProductRequest> products = _productProxy.GetAll();
            int recordsTotal = products.Count();
            int recordsFilterd = recordsTotal;

            //if (!string.IsNullOrEmpty(dataRequest.Search?.Value))
            //{
            //    products = products.Where(e => e.ProductName.Contains(dataRequest.Search.Value));
            //    recordsFilterd = products.Count();
            //}
            products = products.Skip(dataRequest.Start).Take(dataRequest.Length);
            var json = Json(products
                .Select(e => new
                {
                    Id = e.Id,
                    ProductName = e.ProductName,
                    UnitPrice = e.UnitPrice,
                    AvailableDiscount = e.AvailableDiscount,
                    CategoryName = e.CategoryName,
                    UnitName = e.UnitName,
                    Status = e.Status.ToString(),
                    IsActive = e.IsActive
                })
                .ToDataTablesResponse(dataRequest, recordsTotal, recordsFilterd));
            return json;
        }
        #endregion

        #region Helper
        private void SetPrerequisite()
        {
            var categories = _categoryProxy.GetAll();
            ViewBag.Categories = categories;

            var units = _unitProxy.GetAll();
            ViewBag.Units = units;
        } 
        #endregion


    }
}