using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ecommerce.Web.Models;
using Ecommerce.Web.Proxy;
using Ecommerce.Web.Helpers;
using System.Net.Http;
using Ecommerce.Model;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;

namespace Ecommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;
        //private readonly TestServiceProxy _Proxy;
        
        public HomeController(ILogger<HomeController> logger, IConfiguration config, IHttpClientFactory httpClient)
        {
            _logger = logger;
            //_Proxy = new TestServiceProxy(config, httpClient, token);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            if (HttpContext != null) 
            {
                var exceptionDetail = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                if(exceptionDetail != null) 
                { 
                    ViewBag.Message = exceptionDetail.Error.Message;
                    _logger.LogError(exceptionDetail.Error.Message);
                }
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
        }
    }
}
