using System.Net;
using System.Net.Http;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Ecommerce.Model;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Ecommerce.Web.Helpers;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Web.Proxy
{
    public abstract class Proxy
    {
        public readonly IHttpClientFactory _client;
        
        public Proxy(IHttpClientFactory httpClient)
        {
            _client = httpClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="token"></param>
        protected T GetRequest<T>(string requestUrl, string token = null)
        {
            return SendRequest<T>(requestUrl, HttpMethod.Get, token);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="token"></param>
        /// <param name="httpContent"></param>
        protected T MakeRequest<T>(string requestUrl, string token, HttpContent httpContent)
        {
            return SendRequest<T>(requestUrl, HttpMethod.Post, token, httpContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="token"></param>
        /// <param name="httpContent"></param>
        /// <returns></returns>
        protected T PutRequest<T>(string requestUrl, string token, HttpContent httpContent)
        {
            return SendRequest<T>(requestUrl, HttpMethod.Put, token, httpContent);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        protected T DeleteRequest<T>(string requestUrl, string token)
        {
            return SendRequest<T>(requestUrl, HttpMethod.Delete, token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="method"></param>
        /// <param name="httpContent"></param>
        protected T SendRequest<T>(string requestUrl, HttpMethod method, string token, HttpContent httpContent = null)
        {
            //try {
                using var client = _client.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(120);
                if (token != null) client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = null;
                switch (method.Method)
                {
                    case "GET":
                        response = client.GetAsync(requestUrl).Result;
                        break;
                    case "POST":
                        response = client.PostAsync(requestUrl, httpContent).Result;
                        break;
                    case "PUT":
                        response = client.PutAsync(requestUrl, httpContent).Result;
                        break;
                    case "DELETE":
                        response = client.DeleteAsync(requestUrl).Result;
                        break;
                }
                return ProcessResponse<T>(response);
            //}
            // catch (Exception ex)
            //{
            //    Debug.Write(ex.Message);
            //    throw ex;
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpResponse"></param>
        private T ProcessResponse<T>(HttpResponseMessage httpResponse)
        {
            switch ((HttpStatusCode)httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                    string stream = httpResponse.Content.ReadAsStringAsync().Result;
                    var output = JsonConvert.DeserializeObject<ServiceDataWrapper<T>>(stream);
                    if (output.Error != null)
                    {
                        throw new EcommerceWebException(output.Error.Count() > 0? string.Join('|', output.Error): null, output.ErrorCode)
                        {
                            Source = httpResponse.RequestMessage.RequestUri.AbsoluteUri
                        };
                    }
                    else
                        return output.value;
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.InternalServerError:
                default:
                    throw new EcommerceWebException(httpResponse.ReasonPhrase, (short)httpResponse.StatusCode)
                    {
                        Source = httpResponse.RequestMessage.RequestUri.AbsoluteUri
                    };
            }
        }
        protected HttpContent GetHttpContent<T>(T obj)
        {
            string stringData = JsonConvert.SerializeObject(obj);
            return new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
        }
    }
}
