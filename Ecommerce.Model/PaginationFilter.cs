using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model
{
    public class PaginationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
