using System;
using System.Collections.Generic;
using System.Net.Http;
using Ecommerce.Model;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Web.Proxy
{
    public class TestServiceProxy : Proxy
    {
        private readonly string _baseUrl;
        private readonly string _token;
        public TestServiceProxy(IConfiguration config, IHttpClientFactory httpClient, string token) : base(httpClient)
        {
            _baseUrl = (!config.GetValue<string>("ServiceUrl").EndsWith("/") 
                        ? config.GetValue<string>("ServiceUrl") + "/" 
                        : config.GetValue<string>("ServiceUrl"));

            _token = token;
        }
        public Register Get()
        {
            return GetRequest<Register>(_baseUrl + ApiRoutes.Test.Get, _token);
        }
        public string Post(Register register)
        {
            return MakeRequest<string>(_baseUrl + ApiRoutes.Test.Post, null, GetHttpContent(register));
        }
        public string GetValues() {
            return GetRequest<string>(_baseUrl + "api/Values/Get", _token);
        }
    }
}