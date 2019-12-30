using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ecommerce.Web.Proxy;
using DataTables.AspNetCore.Mvc.Binder;
using Ecommerce.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Web.Controllers
{
    public class OrderController : Controller
    {

        #region Declaration
        private readonly ProductProxy _productProxy;
        private readonly OrderProxy _orderProxy;
        #endregion

        #region Constructor
        public OrderController(ILogger<HomeController> logger
                                , IConfiguration config, IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            var token = accessor.HttpContext.User.Claims.First(x => x.Type == "token").Value;
            this._productProxy = new ProductProxy(config, httpClient, token);
            this._orderProxy = new OrderProxy(config, httpClient, token);
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.OrderStatus = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().Select(v => new SelectListItem
                                    {
                                        Text = v.ToString(),
                                        Value = ((int)v).ToString()
                                    }).ToList();
            var details = _orderProxy.Get(id);
            return View(details);
        }
        [HttpPost]
        public IActionResult Update(int id, UpdateOrderRequest request)
        {
            _orderProxy.UpdateOrderStatus(id, request);
            return RedirectToAction("List", "Order");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRequest"></param>
        /// <returns></returns>
        public IActionResult GetOrderList([DataTablesRequest] DataTablesRequest dataRequest)
        {
            IEnumerable<OrderRequest> orders = _orderProxy.GetAll();
            int recordsTotal = orders.Count();
            int recordsFilterd = recordsTotal;

            //if (!string.IsNullOrEmpty(dataRequest.Search?.Value))
            //{
            //    orders = orders.Where(e => e.OrderNo.Contains(dataRequest.Search.Value));
            //    recordsFilterd = orders.Count();
            //}
            orders = orders.Skip(dataRequest.Start).Take(dataRequest.Length);
            var json = Json(orders
                .Select(e => new
                {
                    Id = e.Id,
                    OrderNo = e.OrderNo,
                    OrderDate = e.OrderDate,
                    TotalAmount = e.TotalAmount,
                    TotalDiscount = e.TotalDiscount,
                    Status  = e.Status.ToString()
                })
                .ToDataTablesResponse(dataRequest, recordsTotal, recordsFilterd));
            return json;
        }
    }
}