using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.BL
{
    public interface IIdentityService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        Task<AuthenticationResult> RegisterAsync(Register register);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<AuthenticationResult> LoginAsync(string email, string password);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userFilter"></param>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        Task<IList<ApplicationUserRequest>> GetApplicationUsersAsync(ApplicationUserFilter userFilter, PaginationFilter paginationFilter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApplicationUserId"></param>
        /// <returns></returns>
        Task<bool> LockUnLockApplicationUser(string ApplicationUserId);
    }
}
