using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Helpers
{
    public class EcommerceWebException: Exception
    {
        public EcommerceWebException(string messsage, short httpStatusCode) : base(messsage)
        {
            this.StatusCode = httpStatusCode;
        }
        public short StatusCode { get; private set; }
    }    
}
