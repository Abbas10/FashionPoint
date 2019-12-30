using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.DAL.DataModels
{
    public class ApplicationRole: IdentityRole<string>
    {
        public ApplicationRole():base() 
        { 

        }
        public ApplicationRole(string role):base(role)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<ApplicationUserRole> UserRoles { get;  set; }
    }
}
