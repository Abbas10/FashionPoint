using Ecommerce.API.Extensions;
using Ecommerce.DAL.BL;
using Ecommerce.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Ecommerce.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Customer)]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        #region Declaration
        private readonly ICartService _service;
        #endregion

        #region Constructor
        public CartController(ICartService service)
        {
            _service = service;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get-items")]
        [HttpGet]
        public ServiceDataWrapper<List<ShoppingCartItem>> GetCartItems()
        {
            return new ServiceDataWrapper<List<ShoppingCartItem>>
            {
                value = _service.GetCartItemsAsyc(HttpContext.GetUserId()).Result
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("add-item")]
        [HttpPost]
        public ServiceDataWrapper<bool> AddItemInCart([FromBody] ShoppingCartRequest cartItem)
        {
            return new ServiceDataWrapper<bool>
            {
                value = _service.AddItemInCart(HttpContext.GetUserId(), cartItem).Result
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        [Route("update-item/{id}")]
        [HttpPut]
        public ServiceDataWrapper<bool> UpdateItemInCart([FromRoute]int id, [FromBody] ShoppingCartRequest cartItem)
        {
            return new ServiceDataWrapper<bool>
            {
                value = _service.UpdateItemInCart(HttpContext.GetUserId(), cartItem).Result
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("remove-item/{id}")]
        [HttpDelete]
        public ServiceDataWrapper<bool> RemoveItem([FromRoute] int id)
        {
            return new ServiceDataWrapper<bool>
            {
                value = _service.RemoveItemFromCart(id, HttpContext.GetUserId()).Result
            };
        }
        #endregion
    }
} 