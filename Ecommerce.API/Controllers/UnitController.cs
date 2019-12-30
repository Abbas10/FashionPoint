using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        #region Declaration
        private readonly IUnitService _service;
        #endregion

        #region Constructor
        public UnitController(IUnitService service)
        {
            _service = service;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get Unit List
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme
            , Roles = "Administrator,Retailer")]
        [Route("get-all")]
        [HttpGet] 
        public ServiceDataWrapper<List<UnitRequest>> GetAll([FromQuery]PaginationFilter pagination = null)
        {
            return new ServiceDataWrapper<List<UnitRequest>>
            {
                value = _service.GetUnitsAsync(pagination).Result
            };
        }

        /// <summary>
        /// Get Unit By its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UnitRequest</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Administrator)]
        [Route("get/{id}")]
        [HttpGet]
        public ServiceDataWrapper<UnitRequest> Get([FromRoute]int id)
        {
            return new ServiceDataWrapper<UnitRequest>
            {
                value = _service.GetUnitById(id).Result
            };
        }

        /// <summary>
        /// Create Unit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Administrator)]
        [Route("create-unit")]
        [HttpPost]
        public ServiceDataWrapper<bool> CreateUnit([FromBody] UnitRequest request)
        {
            request.CreatedBy = HttpContext.GetUserId();
            return new ServiceDataWrapper<bool>
            {
                value = _service.CreateUnitAsync(request).Result
            };
        }

        /// <summary>
        /// Update Unit
        /// </summary>
        /// <param name="request"></param>
        /// <returns>true/false</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Administrator)]
        [Route("update-unit/{id}")]
        [HttpPut]
        public ServiceDataWrapper<bool> UpdateUnit([FromRoute]int id, [FromBody] UnitRequest request)
        {
            request.Id = id;
            request.ModifiedBy = HttpContext.GetUserId();
            return new ServiceDataWrapper<bool>
            {
                value = _service.UpdateUnitAsync(request).Result
            };
        }

        /// <summary>
        /// Delete Unit
        /// </summary>
        /// <param name="id">Unit Id</param>
        /// <returns>true/false</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Administrator)]
        [Route("delete/{id}")]
        [HttpDelete]
        public ServiceDataWrapper<bool> Delete([FromRoute]int id)
        {
            return new ServiceDataWrapper<bool>
            {
                value = _service.DeleteUnitAsync(id).Result
            };
        }
        #endregion
    }
}