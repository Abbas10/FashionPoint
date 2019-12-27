using Ecommerce.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ecommerce.Web.Proxy
{
    public class ProductProxy : Proxy
    {
        #region Declaration
        private readonly string _baseUrl;
        private readonly string _token;
        #endregion

        #region Constructor
        public ProductProxy(IConfiguration config, IHttpClientFactory httpClient, string token) : base(httpClient)
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
        public List<ProductRequest> GetAll()
        {
            return GetRequest<List<ProductRequest>>(_baseUrl + ApiRoutes.Product.GetAll, _token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductRequest Get(int id)
        {
            return GetRequest<ProductRequest>(_baseUrl + string.Format(ApiRoutes.Product.Get, id), _token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool CreateProduct(ProductRequest request)
        {
            return MakeRequest<bool>(_baseUrl + ApiRoutes.Product.CreateProduct, _token, GetHttpContent(request));
        }

        public bool UpdateProduct(int id, ProductRequest request)
        {
            return PutRequest<bool>(_baseUrl + string.Format(ApiRoutes.Product.UpdateProduct, id), _token, GetHttpContent(request));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteProduct(int id)
        {
            return DeleteRequest<bool>(_baseUrl + string.Format(ApiRoutes.Product.Delete, id), _token);
        }
        #endregion

        #region Helper Methods
        #endregion
    }
}
