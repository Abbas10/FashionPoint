using Ecommerce.DAL.DataModels;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIdentityRepository
    {
        /// <summary>
        /// Register the Retailer/Customer
        /// </summary>
        /// <param name="newUser">Application User</param>
        /// <param name="password">Password</param>
        /// <param name="userRole">User Role</param>
        /// <returns></returns>
        Task<AuthenticationResult> RegisterAsync(ApplicationUser newUser, string password, string userRole);

        /// <summary>
        /// This endpoint is used to get the User Authentication token
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        Task<AuthenticationResult> LoginAsync(string email, string password);

        /// <summary>
        /// Refresh user authentication token
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="refreshToken">Refresh Token</param>
        /// <returns></returns>
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userFilter">User Filter</param>
        /// <param name="paginationFilter">Pagination Filter</param>
        /// <returns></returns>
        Task<IList<ApplicationUser>> GetApplicationUsersAsync(ApplicationUserFilter userFilter, PaginationFilter paginationFilter);

        /// <summary>
        /// Lock/Unlock the Application user
        /// </summary>
        /// <param name="ApplicationUserId">Customer Id / Retailer Id</param>
        /// <returns></returns>
        Task<bool> LockUnLockApplicationUser(string ApplicationUserId);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> ConfirmEmail(string id, string token);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> IsUserLockedByAdmin(string id);
    }
}
