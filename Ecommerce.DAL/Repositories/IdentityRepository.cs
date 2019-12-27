using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DAL.DataModels;
using Ecommerce.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.DAL.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityRepository : IIdentityRepository
    {
        #region Declaration
        private readonly EcommerceDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        #endregion

        #region Constructor
        /// <summary>
        /// Identity Instance constructor
        /// </summary>
        /// <param name="context">Ecommerce Db Context</param>
        /// <param name="userManager">Application User Manager </param>
        /// <param name="roleManager">Role Manager</param>
        /// <param name="jwtSettings">Jwt settings</param>
        /// <param name="tokenValidationParameters">Token Validator Parameters</param>
        public IdentityRepository(EcommerceDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager
            , JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;

        }
        #endregion

        #region Methods
        /// <summary>
        /// Register the Retailer/Customer
        /// </summary>
        /// <param name="newUser">Application User</param>
        /// <param name="password">Password</param>
        /// <param name="userRole">User Role</param>
        /// <returns></returns>
        public async Task<AuthenticationResult> RegisterAsync(ApplicationUser newUser, string password, string userRole)
        {
            var userExists = await _userManager.FindByEmailAsync(newUser.Email);
            if(userExists != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User already exists" }
                };
            }
            
            var createUser = await _userManager.CreateAsync(newUser, password);

            if (!createUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createUser.Errors.Select(x => x.Description)
                };
            }
            else
            {
                var role = await _userManager.AddToRoleAsync(newUser, userRole);
                if (!role.Succeeded)
                {
                    return new AuthenticationResult
                    {
                        Errors = role.Errors.Select(x => x.Description)
                    };
                }
            }
            return await GenerateAuthenticationResultForUserAsync(newUser);
        }


        /// <summary>
        /// This endpoint is used to get the User Authentication token
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }
            else if (!user.EmailConfirmed)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Email Address is not verified." }
                };
            }
            else if (await _userManager.IsLockedOutAsync(user))
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User is Locked." }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/password combination is wrong" }
                };
            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }


        /// <summary>
        /// Refresh user authentication token
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="refreshToken">Refresh Token</param>
        /// <returns></returns>
        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been invalidated" } };
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not match this JWT" } };
            }

            storedRefreshToken.Used = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            return await GenerateAuthenticationResultForUserAsync(user);
        }

        
            
        /// <summary>
        /// Lock/Unlock the Application user
        /// </summary>
        /// <param name="applicationUserId">Customer Id / Retailer Id</param>
        /// <param name="isLocked">Is Locked</param>
        /// <returns></returns>
        public async Task<bool> LockUnLockApplicationUser(string applicationUserId) 
        {
            var user = await _userManager.FindByIdAsync(applicationUserId);
            bool isLocked = await _userManager.IsLockedOutAsync(user);
            var result = await _userManager.SetLockoutEnabledAsync(user, !isLocked);
            if (isLocked)
                await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddDays(-1));
            else
                await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddMinutes(15));
            return result.Succeeded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <param name="userFilter">User Filter</param>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        public async Task<IList<ApplicationUser>> GetApplicationUsersAsync(ApplicationUserFilter userFilter, PaginationFilter paginationFilter)
        {
            var  query =  _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsQueryable();
            query = AddFiltersOnQuery(userFilter, query);
            
            if (paginationFilter == null)
            {
                return await query.ToListAsync();
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await query.Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
        }
        #endregion

        #region Helper Method

        /// <summary>
        /// Get Principal from token by user authentication token
        /// </summary>
        /// <param name="token">user authentication token</param>
        /// <returns></returns>
        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        
        /// <summary>
        /// Check the token is valid or not
        /// </summary>
        /// <param name="validatedToken">Security Token</param>
        /// <returns></returns>
        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }


        /// <summary>
        /// Generate the Token
        /// </summary>
        /// <param name="user">Identity user</param>
        /// <returns>Authentication Result</returns>
        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null) continue;
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                        continue;

                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        private static IQueryable<ApplicationUser> AddFiltersOnQuery(ApplicationUserFilter filter, IQueryable<ApplicationUser> query)
        {
            /*
             * this filter is used to filter application user
             */
            query = (!string.IsNullOrEmpty(filter?.UserName)) ? query.Where(x => x.UserName.Contains(filter.UserName)) : query;
            return query;
        }
        #endregion

    }
}
