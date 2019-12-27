using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Model
{
    public class RefreshTokenRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RefreshToken { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ApplicationUserRequest 
    { 
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ContactNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLocked { get; set; }
    }
    public class ApplicationUserFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Role { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationUserLockUnlock
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsLocked { get; set; }
    }
}
