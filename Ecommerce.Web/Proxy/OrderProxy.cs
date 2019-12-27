using Ecommerce.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ecommerce.Web.Proxy
{
    public class OrderProxy : Proxy
    {
        #region Declaration
        private readonly string _baseUrl;
        private readonly string _token;
        #endregion

        #region Constructor
        public OrderProxy(IConfiguration config, IHttpClientFactory httpClient, string token) : base(httpClient)
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
        public List<OrderRequest> GetAll()
        {
            return GetRequest<List<OrderRequest>>(_baseUrl + ApiRoutes.Order.GetAll, _token);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderRequest Get(int id)
        {
            return GetRequest<OrderRequest>(_baseUrl + string.Format(ApiRoutes.Order.Get, id), _token);
        }
        #endregion

        #region Helper Methods

        #endregion
    }
}
