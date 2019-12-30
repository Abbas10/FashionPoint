using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Model
{
    public class CategoryRequest : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary> 
        /// 
        /// </summary>
        [Required]
        public string CategoryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; } = true;

        

    }

}
