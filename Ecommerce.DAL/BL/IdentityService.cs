using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DAL.DataModels;
using Ecommerce.DAL.Repositories;
using Ecommerce.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.DAL.BL
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityService : IIdentityService
    {
        #region Declaration
        private readonly IIdentityRepository _repository;
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository">Identity Repository</param>
        public IdentityService(IIdentityRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> RegisterAsync(Register register)
        {
            ApplicationUser applicationUser;
            switch (register.Role)
            {
                case ApplicationConstant.ApplicationRoles.Customer:
                    CustomerRegister customerRegister = (CustomerRegister)register;
                    applicationUser = new ApplicationUser
                    {
                        UserName = customerRegister.Email,
                        Email = customerRegister.Email,
                        ContactNo = customerRegister.ContactNo,
                        AddressLine1 = customerRegister.AddressLine1,
                        AddressLine2 = customerRegister.AddressLine2,
                        City = customerRegister.City,
                        State = customerRegister.State,
                        Zipcode = customerRegister.Zipcode
                    };
                    return await _repository.RegisterAsync(applicationUser, register.Password, register.Role);
                case ApplicationConstant.ApplicationRoles.Retailer:
                    RetailerRegister retailerRegister = (RetailerRegister)register;
                    applicationUser = new ApplicationUser
                    {
                        UserName = retailerRegister.Email,
                        Email = retailerRegister.Email,
                        ContactNo = retailerRegister.ContactNo,
                        BusinessEmail = retailerRegister.BusinessEmail,
                        AddressLine1 = retailerRegister.BusinessAddressLine1,
                        AddressLine2 = retailerRegister.BusinessAddressLine2,
                        City = retailerRegister.City,
                        State = retailerRegister.State,
                        Zipcode = retailerRegister.Zipcode
                    };
                    return await _repository.RegisterAsync(applicationUser, register.Password, register.Role);
            }
            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            return await _repository.LoginAsync(email, password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            return await _repository.RefreshTokenAsync(token, refreshToken);
        }
        public async Task<IList<ApplicationUserRequest>> GetApplicationUsersAsync(ApplicationUserFilter userFilter, PaginationFilter paginationFilter)
        {
            var users = await _repository.GetApplicationUsersAsync(userFilter, paginationFilter);
            return users.Select(x => new ApplicationUserRequest
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                ContactNo = x.ContactNo,
                Role = x.UserRoles.FirstOrDefault()?.Role?.Name, 
                IsLocked = x.LockoutEnabled
            }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApplicationUserId"></param>
        /// <param name="isLocked"></param>
        /// <returns></returns>
        public async Task<bool> LockUnLockApplicationUser(string ApplicationUserId) 
        {
            return await _repository.LockUnLockApplicationUser(ApplicationUserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<bool> ConfirmEmail(string id, string token)
        {
            return _repository.ConfirmEmail(id, token);
        }

        public Task<bool> IsUserLockedByAdmin(string id)
        {
            return _repository.IsUserLockedByAdmin(id);
        }
        #endregion
    }
}
