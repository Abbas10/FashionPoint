using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model
{
    /// <summary>
    /// Base class of registration
    /// </summary>
    public class Register
    {
        public Register(string role)
        { 
            this.Role = role;
        }
        /// <summary>
        /// Name of Register user
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email address of Register user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password of Register user
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Contact no. of register user
        /// </summary>
        public string ContactNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Role { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Zipcode { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class CustomerRegister: Register
    {
        public CustomerRegister():base(ApplicationConstant.ApplicationRoles.Customer)
        {
            
        }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class RetailerRegister: Register
    {
        /// <summary>
        /// 
        /// </summary>
        public RetailerRegister():base(ApplicationConstant.ApplicationRoles.Retailer)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        public string BusinessEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BusinessAddressLine1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BusinessAddressLine2 { get; set; }

    }
}
