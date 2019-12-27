using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce.Web.Helpers
{
    public static class JWTTokenDecoder
    {
        public static IEnumerable<Claim> Decode(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenS = (handler.ReadToken(token) as JwtSecurityToken);
            return tokenS.Claims;
        }
    }
}
