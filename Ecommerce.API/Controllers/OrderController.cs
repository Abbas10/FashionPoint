using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce.API.Extensions;
using Ecommerce.DAL.BL;
using Ecommerce.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Declaration
        private readonly IOrderService _service;
        #endregion

        #region Constructor
        public OrderController(IOrderService service)
        {
            _service = service;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get Order List
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        [Route("get-all")]
        [HttpGet]
        public ServiceDataWrapper<List<OrderRequest>> GetAll([FromQuery]OrderFilter orderFilter, [FromQuery]PaginationFilter pagination = null)
        {
            if(User.Claims.Any(x=> x.Type == ClaimTypes.Role && x.Value  == ApplicationConstant.ApplicationRoles.Customer))
                orderFilter.CustomerId = HttpContext.GetUserId();
            else
                orderFilter.RetailerId = HttpContext.GetUserId();
            return new ServiceDataWrapper<List<OrderRequest>>
            {
                value = _service.GetOrdersAsync(orderFilter, pagination).Result
            };
        }

        /// <summary>
        /// Get Order By its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderRequest</returns>
        [Route("get/{id}")]
        [HttpGet]
        public ServiceDataWrapper<OrderRequest> Get([FromRoute]int id)
        {
            var orderFilter = new OrderFilter { CustomerId = HttpContext.GetUserId() };
            return new ServiceDataWrapper<OrderRequest>
            {
                value = _service.GetOrderById(id, orderFilter).Result
            };
        }

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = ApplicationConstant.ApplicationRoles.Customer)]
        [Route("create-order")]
        [HttpPost]
        public ServiceDataWrapper<bool> CreateOrder([FromBody] List<CustomerOrderedProductRequest> request)
        {
            return new ServiceDataWrapper<bool>
            {
                value = _service.CreateOrderAsync(request, HttpContext.GetUserId()).Result
            };
        }

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="request"></param>
        /// <returns>true/false</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Retailer)]
        [Route("update-order/{id}")]
        [HttpPut]
        public ServiceDataWrapper<bool> UpdateOrder([FromRoute]int id, [FromBody] UpdateOrderRequest request)
        {
            return new ServiceDataWrapper<bool>
            {
                value = _service.UpdateOrderAsync(id, request.OrderStatus, HttpContext.GetUserId()).Result
            };
        }

        /// <summary>
        /// Delete Order
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>true/false</returns>
        [Route("delete/{id}")]
        [HttpDelete]
        public ServiceDataWrapper<bool> Delete([FromRoute]int id)
        {
            var orderFilter = new OrderFilter { CustomerId = HttpContext.GetUserId() };
            return new ServiceDataWrapper<bool>
            {
                value = _service.DeleteOrderAsync(id, orderFilter).Result
            };
        }
        #endregion
    }
}