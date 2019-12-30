using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class ProductController : ControllerBase
    {
        #region Declaration
        private readonly IProductService _service;
        #endregion

        #region Constructor
        public ProductController(IProductService service)
        {
            _service = service;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get Product List
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer,Retailer")]
        [Route("get-all")]
        [HttpGet]
        public ServiceDataWrapper<List<ProductRequest>> GetAll([FromQuery]ProductFilter productFilter, [FromQuery]PaginationFilter pagination = null)
        {
            if (HttpContext.GetRole().Equals(ApplicationConstant.ApplicationRoles.Retailer))
            {
                productFilter.CreatedBy = HttpContext.GetUserId();
            }
            else
            {
                productFilter.Status = ProductStatus.InStock;
            }
            return new ServiceDataWrapper<List<ProductRequest>>
            {
                value = _service.GetProductsAsync(productFilter, pagination).Result
            };
        }

        /// <summary>
        /// Get Product By its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ProductRequest</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer,Retailer")]
        [Route("get/{id}")]
        [HttpGet]
        public ServiceDataWrapper<ProductRequest> Get([FromRoute]int id)
        {
            var productFilter = new ProductFilter { CreatedBy = HttpContext.GetUserId() };
            var product = _service.GetProductById(id, productFilter).Result;
            if (product != null)
                return new ServiceDataWrapper<ProductRequest> { value = product };
            else
                return new ServiceDataWrapper<ProductRequest> { Error = new string[] { "Product not found" } , ErrorCode = (short) HttpStatusCode.BadRequest };
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Retailer)]
        [Route("create-product")]
        [HttpPost]
        public ServiceDataWrapper<bool> CreateProduct([FromBody] ProductRequest request)
        {
            request.CreatedBy = HttpContext.GetUserId();
            return new ServiceDataWrapper<bool>
            {
                value = _service.CreateProductAsync(request).Result
            };
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="request"></param>
        /// <returns>true/false</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Retailer)]
        [Route("update-product/{id}")]
        [HttpPut]
        public ServiceDataWrapper<bool> UpdateProduct([FromRoute]int id, [FromBody] ProductRequest request)
        {
            request.Id = id;
            request.ModifiedBy = HttpContext.GetUserId();
            var productFilter = new ProductFilter { CreatedBy = HttpContext.GetUserId() };
            return new ServiceDataWrapper<bool>
            {
                value = _service.UpdateProductAsync(request, productFilter).Result
            };
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>true/false</returns>
        [Route("delete/{id}")]
        [HttpDelete]
        public ServiceDataWrapper<bool> Delete([FromRoute]int id)
        {
            var productFilter = new ProductFilter { CreatedBy = HttpContext.GetUserId() };
            return new ServiceDataWrapper<bool>
            {
                value = _service.DeleteProductAsync(id, productFilter).Result
            };
        }
        #endregion
    }
}