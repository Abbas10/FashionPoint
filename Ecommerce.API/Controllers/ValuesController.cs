using Ecommerce.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebService.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        #region Constructor
        public ValuesController()
        {

        }
        #endregion

        [Route("Get")]
        [HttpGet]
        public ServiceDataWrapper<string> Get()
        {
            return new ServiceDataWrapper<string>
            {
                value = "dfsadfsadfsa" 
            };
        }
    }
}
