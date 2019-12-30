using Ecommerce.API.Extensions;
using Ecommerce.DAL.BL;
using Ecommerce.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Declaration
        private readonly ICategoryService _service;
        #endregion

        #region Constructor
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get Category List
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator,Retailer")]
        //, Roles = string.Format("{0},{1}", ApplicationConstant.ApplicationRoles.Administrator, ApplicationConstant.ApplicationRoles.Retailer))]
        [Route("get-all")]
        [HttpGet]
        public ServiceDataWrapper<List<CategoryRequest>> GetAll([FromQuery]PaginationFilter pagination = null)
        {
            return new ServiceDataWrapper<List<CategoryRequest>>
            {
                value = _service.GetCategoriesAsync(pagination).Result
            };
        }

        /// <summary>
        /// Get Category By its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CategoryRequest</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Administrator)]
        [Route("get/{id}")]
        [HttpGet]
        public ServiceDataWrapper<CategoryRequest> Get([FromRoute]int id)
        {
            return new ServiceDataWrapper<CategoryRequest>
            {
                value = _service.GetCategoryById(id).Result
            };
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Administrator)]
        [Route("create-category")]
        [HttpPost]
        public ServiceDataWrapper<bool> CreateCategory([FromBody] CategoryRequest request)
        {
            request.CreatedBy = HttpContext.GetUserId();
            return new ServiceDataWrapper<bool> 
            { 
                value = _service.CreateCategoryAsync(request).Result
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Administrator)]
        [Route("update-category/{id}")]
        [HttpPut]
        public ServiceDataWrapper<bool> UpdateCategory([FromRoute]int id, [FromBody] CategoryRequest request)
        {
            request.Id = id;
            request.ModifiedBy = HttpContext.GetUserId();
            return new ServiceDataWrapper<bool>
            {
                value = _service.UpdateCategoryAsync(request).Result
            };
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Administrator)]
        [Route("delete/{id}")]
        [HttpDelete]
        public ServiceDataWrapper<bool> Delete([FromRoute]int id)
        {
            return new ServiceDataWrapper<bool>
            {
                value = _service.DeleteCategoryAsync(id).Result
            };
        }
       
        #endregion
    }
}