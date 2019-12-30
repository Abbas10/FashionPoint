using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.DAL.DataModels
{
    public class Order
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime OrderDate { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalDiscount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short Status { get; set; }

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
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}
