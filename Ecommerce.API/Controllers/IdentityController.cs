using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.DAL.BL;
using Ecommerce.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebService.Controllers
{
    /// <summary>
    /// Identity Controller is used to Register, User authentication etc.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        #region Declaration
        private readonly IIdentityService _service;
        #endregion

        #region Constructor
        public IdentityController(IIdentityService service)
        {
            _service = service;
        }
        #endregion

        #region Methods

        /// <summary>
        /// This endpoint is for create/register the customer 
        /// </summary>
        /// <param name="register">Customer register</param>
        /// <returns>Authentication Result</returns>
        [Route("customer-registration")]
        [HttpPost]
        public ServiceDataWrapper<AuthenticationResult> CustomerRegistration(CustomerRegister register)
        {
            return new ServiceDataWrapper<AuthenticationResult>
            {
                value = _service.RegisterAsync(register).Result
            };
        }


        /// <summary>
        /// this endpoint is for create/register the retailer
        /// </summary>
        /// <param name="register">Retailer register</param>
        /// <returns>Authentication Result</returns>
        [Route("retailer-registration")]
        [HttpPost]
        public ServiceDataWrapper<AuthenticationResult> RetailerRegistration(RetailerRegister register)
        {
            return new ServiceDataWrapper<AuthenticationResult>
            {
                value = _service.RegisterAsync(register).Result
            };
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request">User Login Request</param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public ServiceDataWrapper<AuthenticationResponse> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = _service.LoginAsync(request.Email, request.Password).Result;

            if (!authResponse.Success)
            {
                return new ServiceDataWrapper<AuthenticationResponse>
                {
                    //ErrorCode = (short)HttpStatusCode.UnavailableForLegalReasons,
                    //Error = authResponse.Errors,
                    value = new AuthenticationResponse
                    {
                        Error = string.Join(',', authResponse.Errors)
                    }
                };
            }

            return new ServiceDataWrapper<AuthenticationResponse>
            {
                value = new AuthenticationResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                }
            };
        }

        /// <summary>
        ///  Refresh user authentication token
        /// </summary>
        /// <param name="request">Refresh Token Request</param>
        /// <returns></returns>
        [Route("refresh")]
        [HttpPost]
        public ServiceDataWrapper<AuthenticationResponse> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = _service.RefreshTokenAsync(request.Token, request.RefreshToken).Result;

            if (!authResponse.Success)
            {
                return new ServiceDataWrapper<AuthenticationResponse>
                {
                    ErrorCode = (short) HttpStatusCode.BadRequest,
                    Error = authResponse.Errors,
                    value = null
                };
            }

            return new ServiceDataWrapper<AuthenticationResponse>
            {
                value = new AuthenticationResponse
                { 
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                }
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userFilter"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Administrator)]
        [Route("get-all")]
        [HttpGet]
        public ServiceDataWrapper<IList<ApplicationUserRequest>> GetAll([FromQuery] ApplicationUserFilter userFilter, [FromQuery]PaginationFilter pagination = null)
        {
            return new ServiceDataWrapper<IList<ApplicationUserRequest>>
            {
                value = _service.GetApplicationUsersAsync(userFilter, pagination).Result
            };
        }


        /// <summary>
        /// Lock Unlock the Application user (Customer / Retailer)
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConstant.ApplicationRoles.Administrator)]
        [Route("lock-unlock/{id}")]
        [HttpPut]
        public ServiceDataWrapper<bool> LockUnlockApplicationUser([FromRoute]string id)
        {
            return new ServiceDataWrapper<bool>
            {
                value = _service.LockUnLockApplicationUser(id).Result
            };
        }
        #endregion
    }
}