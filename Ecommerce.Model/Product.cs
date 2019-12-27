using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Model
{
    public class ProductRequest : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string ProductName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? AvailableDiscount { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public ProductStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int UnitId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; }

        public bool InStock { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

    }
    public enum ProductStatus
    {
        /// <summary>
        /// 
        /// </summary>
        InStock = 1,

        /// <summary>
        /// 
        /// </summary>
        OutOfStock
    }
    public class ProductFilter
    {
        public string CreatedBy { get; set; }

        public bool? IsActive { get; set; } 

        public int[] ProductIds { get; set; }
    }
}
