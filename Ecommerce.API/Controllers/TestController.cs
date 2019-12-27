using Ecommerce.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Ecommerce.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Route("HelloWorld")]
        public string HelloWorld() 
        {
            return "Hello world";
        }
        [Route("Get")]
        [HttpGet]
        public ServiceDataWrapper<Register> Get()
        {
            //var s = 0;
            //var a = 1 / s;
            return new ServiceDataWrapper<Register>
            {
                value = new CustomerRegister
                {
                    Email = "abc@gamil.com",
                    Password = "asdf",
                    ContactNo = "54455454"
                }
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [Route("Post")]
        [HttpPost]
        public ServiceDataWrapper<string> Post(Register register)
        {
            return new ServiceDataWrapper<string> { value = "success" };
        }
    }
}