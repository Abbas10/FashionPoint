using Ecommerce.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ecommerce.Web.Proxy
{
    public class UnitProxy : Proxy
    {
        #region Declaration
        private readonly string _baseUrl;
        private readonly string _token;
        #endregion

        #region Constructor
        public UnitProxy(IConfiguration config, IHttpClientFactory httpClient, string token) : base(httpClient)
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
        /// <returns></returns>
        public List<UnitRequest> GetAll()
        {
            return GetRequest<List<UnitRequest>>(_baseUrl + ApiRoutes.Unit.GetAll, _token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UnitRequest Get(int id)
        {
            return GetRequest<UnitRequest>(_baseUrl + string.Format(ApiRoutes.Unit.Get, id), _token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool CreateUnit(UnitRequest request)
        {
            return MakeRequest<bool>(_baseUrl + ApiRoutes.Unit.CreateUnit, _token, GetHttpContent(request));
        }

        public bool UpdateUnit(int id, UnitRequest request)
        {
            return PutRequest<bool>(_baseUrl + string.Format(ApiRoutes.Unit.UpdateUnit, id), _token, GetHttpContent(request));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUnit(int id)
        {
            return DeleteRequest<bool>(_baseUrl + string.Format(ApiRoutes.Unit.Delete, id), _token);
        }
        #endregion

        #region Helper Methods
        #endregion
    }
}
