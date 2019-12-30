using Ecommerce.Model;
using Ecommerce.Web.Proxy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Ecommerce.Web.Controllers
{
    public class CartController : Controller
    {
        #region Declaration
        private readonly CartProxy _cartProxy;
        private readonly OrderProxy _orderProxy;
        #endregion

        #region Constructor
        public CartController(ILogger<HomeController> logger
                                , IConfiguration config, IHttpClientFactory httpClient, IHttpContextAccessor accessor
                                , IWebHostEnvironment hostingEnvironment)
        {
            var token = accessor.HttpContext.User.Claims.First(x => x.Type == "token").Value;
            this._cartProxy = new CartProxy(config, httpClient, token);
            this._orderProxy = new OrderProxy(config, httpClient, token);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckOut()
        {
            var response = _cartProxy.GetItems();
            return View(response);
        }
        [HttpPost]
        public IActionResult CheckOut(ICollection<ShoppingCartRequest> Items)
        {
            if (_orderProxy.CreateOrder(Items.ToList()))
                return RedirectToAction("List", "Order");
            return RedirectToAction("CheckOut");
        }

        [HttpPost]
        public IActionResult AddItem(ShoppingCartRequest cartItem)
        {
            var flag = _cartProxy.AddItem(cartItem);
            return Json(new { data = flag , error = "" });
        }
        public IActionResult RemoveItem(int id)
        {
            _cartProxy.RemoveItem(id);
            return RedirectToAction("CheckOut");
        }
    }
}