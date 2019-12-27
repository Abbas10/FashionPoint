using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Ecommerce.Model;
using System.Collections.Generic;

namespace Ecommerce.Web.Proxy
{
    public class CategoryProxy : Proxy
    {
        #region Declaration
        private readonly string _baseUrl;
        private readonly string _token;
        #endregion

        #region Constructor
        public CategoryProxy(IConfiguration config, IHttpClientFactory httpClient, string token) : base(httpClient)
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
        public List<CategoryRequest> GetAll()
        {
            return GetRequest<List<CategoryRequest>>(_baseUrl + ApiRoutes.Category.GetAll, _token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryRequest Get(int id)
        {
            return GetRequest<CategoryRequest>(_baseUrl + string.Format(ApiRoutes.Category.Get, id), _token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool CreateCategory(CategoryRequest request)
        {
            return MakeRequest<bool>(_baseUrl + ApiRoutes.Category.CreateCategory, _token, GetHttpContent(request));
        }

        public bool UpdateCategory(int id, CategoryRequest request)
        {
            return PutRequest<bool>(_baseUrl + string.Format(ApiRoutes.Category.UpdateCategory, id), _token, GetHttpContent(request));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteCategory(int id)
        {
            return DeleteRequest<bool>(_baseUrl + string.Format(ApiRoutes.Category.Delete, id), _token);
        }
        #endregion

        #region Helper Methods
        #endregion
    }
}
