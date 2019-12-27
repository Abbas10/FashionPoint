using System.Net.Http;
using Ecommerce.Model;
using Ecommerce.Web.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Web.Helpers;
using System.Linq;

namespace Ecommerce.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Declaration
        private readonly IdentityServiceProxy _proxy;
        private readonly ILogger<HomeController> _logger;
        
        #endregion

        #region Constructor
        public AccountController(ILogger<HomeController> logger, IConfiguration config, IHttpClientFactory httpClient)
        {
            _logger = logger;
            _proxy = new IdentityServiceProxy(config, httpClient );
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.Message = (TempData["Message"] != null) ? TempData["Message"].ToString() : string.Empty;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequest loginRequest, string ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var authResponse = _proxy.Login(loginRequest);
                if (string.IsNullOrEmpty(authResponse.Error)) 
                {
                    var claims = JWTTokenDecoder.Decode(authResponse.Token).ToList();
                    claims.Add(new Claim("token", authResponse.Token));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
                                                    , new ClaimsPrincipal(claimsIdentity)
                                                    , new AuthenticationProperties());
                    

                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);

                    
                    //switch (claims.First(x=> x.Type == "role").Value)
                    //{
                    //    case ApplicationConstant.ApplicationRoles.Administrator:
                    //        return RedirectToAction("list", "category");
                    //    case ApplicationConstant.ApplicationRoles.Retailer:
                    //        return RedirectToAction("list", "product");
                    //    case ApplicationConstant.ApplicationRoles.Customer:
                    //        return RedirectToAction("home", "index");
                    //}
                    return RedirectToAction("index", "home");
                    
                }
                else {
                    ViewBag.Message = authResponse.Error;
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult CustomerRegistration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerRegistration(CustomerRegister register)
        {
            var authResponse = _proxy.CustomerRegistration(register);
            if (string.IsNullOrEmpty(authResponse.Error)) {
                TempData["Message"] = "Verify your email address";
                return Redirect("/account/login");
            }
            return View();
        }
        [HttpGet]
        public IActionResult RetailerRegistration()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult RetailerRegistration(RetailerRegister register)
        {
            var authResponse = _proxy.RetailerRegistration(register);
            if (string.IsNullOrEmpty(authResponse.Error))
            {
                TempData["Message"] = "Verify your email address";
                return Redirect("/account/login");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/home/index");
        }
        #endregion

        #region Helper Methods

        #endregion
    }
}