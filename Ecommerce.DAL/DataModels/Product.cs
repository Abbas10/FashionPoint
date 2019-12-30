using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL.DataModels
{
    public class Product
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
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
        public decimal AvailableDiscount { get; set; } = 0;

        /// <summary>
        /// Product Status
        /// </summary>
        public short Status { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(UnitId))]
        public Unit Unit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(CreatedBy))]
        public ApplicationUser CreatedByUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(ModifiedBy))]
        public ApplicationUser ModifiedByUser { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
