using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model
{
    public abstract class BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
