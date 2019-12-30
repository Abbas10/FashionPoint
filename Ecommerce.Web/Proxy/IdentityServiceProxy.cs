using Ecommerce.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ecommerce.Web.Proxy
{
    public class IdentityServiceProxy : Proxy
    {
        #region Declaration
        private readonly string _baseUrl;
        private readonly string _token;
        #endregion

        #region Constructor
        public IdentityServiceProxy(IConfiguration config, IHttpClientFactory httpClient, string token = null) : base(httpClient)
        {
            _baseUrl = (!config.GetValue<string>("ServiceUrl").EndsWith("/")
                       ? config.GetValue<string>("ServiceUrl") + "/"
                       : config.GetValue<string>("ServiceUrl"));
            _token = token;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public AuthenticationResponse Login(UserLoginRequest loginRequest)
        {
            return MakeRequest<AuthenticationResponse>(_baseUrl + ApiRoutes.Identity.Login, null, GetHttpContent(loginRequest));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public AuthenticationResult CustomerRegistration(CustomerRegister register)
        {
            return MakeRequest<AuthenticationResult>(_baseUrl + ApiRoutes.Identity.CustomerRegistration, null, GetHttpContent(register));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public AuthenticationResult RetailerRegistration(RetailerRegister register)
        {
            return MakeRequest<AuthenticationResult>(_baseUrl + ApiRoutes.Identity.RetailerRegistration, null, GetHttpContent(register));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUserRequest> GetAllUser()
        {
            return GetRequest<List<ApplicationUserRequest>>(_baseUrl + ApiRoutes.Identity.GetAll, _token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool LockUnlock(string id)
        {
            return PutRequest<bool>(_baseUrl + string.Format(ApiRoutes.Identity.LockUnlock, id), _token, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ConfirmEmail(string id, EmailVerificationRequest request)
        {
            return PutRequest<bool>(_baseUrl + string.Format(ApiRoutes.Identity.ConfirmEmail, id), null, GetHttpContent(request));
        }
        #endregion

        #region Helper Methods
        #endregion
    }
}
