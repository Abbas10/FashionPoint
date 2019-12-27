using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.DAL.DataModels
{
    public class OrderDetail
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }
        
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
        public int OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

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
        public short Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalDiscount { get; set; }
        /// <summary>
        /// For Order Line
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(ModifiedBy))]
        public ApplicationUser ModifiedByUser { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
