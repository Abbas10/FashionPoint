using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ecommerce.Model;

namespace Ecommerce.Web.Proxy
{
    public class CartProxy : Proxy
    {
        #region Declaration
        private readonly string _baseUrl;
        private readonly string _token;
        #endregion

        #region Constructor
        public CartProxy(IConfiguration config, IHttpClientFactory httpClient, string token) : base(httpClient)
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
        public List<ShoppingCartItem> GetItems()
        {
            return GetRequest<List<ShoppingCartItem>>(_baseUrl + string.Format(ApiRoutes.Cart.GetItems) , _token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        public bool AddItem(ShoppingCartRequest cartItem)
        {
            return MakeRequest<bool>(_baseUrl + ApiRoutes.Cart.AddItem,_token, GetHttpContent(cartItem));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        public bool UpdateItem(int id, ShoppingCartItem cartItem)
        {
            return PutRequest<bool>(_baseUrl + string.Format(ApiRoutes.Cart.UpdateItem, id), _token, GetHttpContent(cartItem));
        }
        public bool RemoveItem(int id)
        {
            return DeleteRequest<bool>(_baseUrl + string.Format(ApiRoutes.Cart.RemoveItem, id), _token);
        }
        #endregion
    }
}
