using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.DataModels
{
    public class ApplicationUser: IdentityUser
    {
        /// <summary>
        /// 
        /// </summary>
        public string ContactNo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string BusinessEmail { get; set; }

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
        
        /// <summary>
        /// 
        /// </summary>
        public string AddressLine1 { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
